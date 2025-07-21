using System.Net.Http.Json;

string apiUrl = "http://localhost:5000";
HttpClient client = new HttpClient { BaseAddress = new Uri(apiUrl) };

while (true)
{
    Console.WriteLine("\n===== MENU ECOMMERCE API =====");
    Console.WriteLine("1. Listar usuários");
    Console.WriteLine("2. Cadastrar usuário");
    Console.WriteLine("3. Listar produtos");
    Console.WriteLine("4. Cadastrar produto");
    Console.WriteLine("5. Criar pedido");
    Console.WriteLine("6. Health Check");
    Console.WriteLine("0. Sair");
    Console.Write("Escolha uma opção: ");
    var op = Console.ReadLine();
    if (op == "0") break;
    try
    {
        switch (op)
        {
            case "1":
                var id = await Prompt("Digite o ID do usuário (ou Enter para listar todos): ");
                if (string.IsNullOrWhiteSpace(id))
                {
                    var users = await client.GetFromJsonAsync<List<UserDto>>("/api/users");
                    foreach (var u in users ?? new())
                        Console.WriteLine($"{u.Id} | {u.Name} | {u.Email}");
                }
                else
                {
                    var user = await client.GetFromJsonAsync<UserDto>($"/api/users/{id}");
                    if (user != null)
                        Console.WriteLine($"{user.Id} | {user.Name} | {user.Email}");
                    else
                        Console.WriteLine("Usuário não encontrado.");
                }
                break;
            case "2":
                var name = await Prompt("Nome: ");
                var email = await Prompt("Email: ");
                var password = await Prompt("Senha: ");
                var resp = await client.PostAsJsonAsync("/api/users", new { name, email, password });
                if (resp.IsSuccessStatusCode)
                    Console.WriteLine("Usuário cadastrado!");
                else
                    Console.WriteLine(await resp.Content.ReadAsStringAsync());
                break;
            case "3":
                var products = await client.GetFromJsonAsync<List<ProductDto>>("/api/products");
                foreach (var p in products ?? new())
                    Console.WriteLine($"{p.Id} | {p.Name} | {p.Price:C} | Estoque: {p.Stock}");
                break;
            case "4":
                var pname = await Prompt("Nome: ");
                var desc = await Prompt("Descrição: ");
                var price = decimal.Parse(await Prompt("Preço: "));
                var stock = int.Parse(await Prompt("Estoque: "));
                var presp = await client.PostAsJsonAsync("/api/products", new { name = pname, description = desc, price, stock });
                if (presp.IsSuccessStatusCode)
                    Console.WriteLine("Produto cadastrado!");
                else
                    Console.WriteLine(await presp.Content.ReadAsStringAsync());
                break;
            case "5":
                var userId = await Prompt("ID do usuário: ");
                var prodId = await Prompt("ID do produto: ");
                var qty = int.Parse(await Prompt("Quantidade: "));
                var orderResp = await client.PostAsJsonAsync("/api/orders", new
                {
                    userId,
                    products = new[] { new { productId = Guid.Parse(prodId), quantity = qty } }
                });
                if (orderResp.IsSuccessStatusCode)
                    Console.WriteLine("Pedido criado!");
                else
                    Console.WriteLine(await orderResp.Content.ReadAsStringAsync());
                break;
            case "6":
                var health = await client.GetStringAsync("/api/health");
                Console.WriteLine(health);
                break;
            default:
                Console.WriteLine("Opção inválida!");
                break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erro: {ex.Message}");
    }
}

static async Task<string> Prompt(string msg)
{
    Console.Write(msg);
    return await Task.FromResult(Console.ReadLine() ?? "");
}

record UserDto(Guid Id, string Name, string Email);
record ProductDto(Guid Id, string Name, string Description, decimal Price, int Stock);
