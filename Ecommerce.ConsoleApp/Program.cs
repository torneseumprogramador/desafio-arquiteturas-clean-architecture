using System.Net.Http.Json;

// Cores ANSI
string RED = "\u001b[31m";
string GREEN = "\u001b[32m";
string YELLOW = "\u001b[33m";
string BLUE = "\u001b[34m";
string CYAN = "\u001b[36m";
string RESET = "\u001b[0m";

string apiUrl = "http://localhost:5158";
HttpClient client = new HttpClient { BaseAddress = new Uri(apiUrl) };

void PrintHeader()
{
    Console.WriteLine($"{CYAN}╔══════════════════════════════════════════════════════╗{RESET}");
    Console.WriteLine($"{CYAN}║         🛒  ECOMMERCE CLEAN ARCHITECTURE CONSOLE     ║{RESET}");
    Console.WriteLine($"{CYAN}╚══════════════════════════════════════════════════════╝{RESET}");
}

while (true)
{
    Console.Clear();
    PrintHeader();
    Console.WriteLine($"{YELLOW}1️⃣  Listar usuários");
    Console.WriteLine($"2️⃣  Cadastrar usuário");
    Console.WriteLine($"3️⃣  Listar produtos");
    Console.WriteLine($"4️⃣  Cadastrar produto");
    Console.WriteLine($"5️⃣  Criar pedido");
    Console.WriteLine($"6️⃣  Health Check");
    Console.WriteLine($"0️⃣  Sair{RESET}");
    Console.WriteLine($"{BLUE}──────────────────────────────────────────────────────{RESET}");
    Console.Write($"{CYAN}Escolha uma opção: {RESET}");
    var op = Console.ReadLine();
    if (op == "0")
    {
        Console.WriteLine($"{GREEN}👋 Obrigado por usar o sistema! Até logo!{RESET}");
        break;
    }
    try
    {
        switch (op)
        {
            case "1":
                Console.WriteLine($"{BLUE}\n👥 Usuários cadastrados:{RESET}");
                var id = await Prompt("Digite o ID do usuário (ou Enter para listar todos): ");
                if (string.IsNullOrWhiteSpace(id))
                {
                    var users = await client.GetFromJsonAsync<List<UserDto>>("/api/users");
                    if (users is { Count: >0 })
                        foreach (var u in users)
                            Console.WriteLine($"{GREEN}🆔 {u.Id} | {u.Name} | {u.Email}{RESET}");
                    else
                        Console.WriteLine($"{YELLOW}⚠️  Nenhum usuário encontrado.{RESET}");
                }
                else
                {
                    var user = await client.GetFromJsonAsync<UserDto>($"/api/users/{id}");
                    if (user != null)
                        Console.WriteLine($"{GREEN}🆔 {user.Id} | {user.Name} | {user.Email}{RESET}");
                    else
                        Console.WriteLine($"{RED}❌ Usuário não encontrado.{RESET}");
                }
                break;
            case "2":
                Console.WriteLine($"{BLUE}\n👤 Cadastro de novo usuário:{RESET}");
                var name = await Prompt("Nome: ");
                var email = await Prompt("Email: ");
                var password = await Prompt("Senha: ");
                var resp = await client.PostAsJsonAsync("/api/users", new { name, email, password });
                if (resp.IsSuccessStatusCode)
                    Console.WriteLine($"{GREEN}✅ Usuário cadastrado com sucesso!{RESET}");
                else
                    Console.WriteLine($"{RED}❌ {await resp.Content.ReadAsStringAsync()}{RESET}");
                break;
            case "3":
                Console.WriteLine($"{BLUE}\n📦 Produtos disponíveis:{RESET}");
                var products = await client.GetFromJsonAsync<List<ProductDto>>("/api/products");
                if (products is { Count: >0 })
                    foreach (var p in products)
                        Console.WriteLine($"{YELLOW}🆔 {p.Id} | {p.Name} | 💲{p.Price:C} | Estoque: {p.Stock}{RESET}");
                else
                    Console.WriteLine($"{YELLOW}⚠️  Nenhum produto encontrado.{RESET}");
                break;
            case "4":
                Console.WriteLine($"{BLUE}\n🆕 Cadastro de novo produto:{RESET}");
                var pname = await Prompt("Nome: ");
                var desc = await Prompt("Descrição: ");
                var price = decimal.Parse(await Prompt("Preço: "));
                var stock = int.Parse(await Prompt("Estoque: "));
                var presp = await client.PostAsJsonAsync("/api/products", new { name = pname, description = desc, price, stock });
                if (presp.IsSuccessStatusCode)
                    Console.WriteLine($"{GREEN}✅ Produto cadastrado com sucesso!{RESET}");
                else
                    Console.WriteLine($"{RED}❌ {await presp.Content.ReadAsStringAsync()}{RESET}");
                break;
            case "5":
                Console.WriteLine($"{BLUE}\n🛒 Criar novo pedido:{RESET}");
                // Listar usuários
                var userList = await client.GetFromJsonAsync<List<UserDto>>("/api/users") ?? new List<UserDto>();
                if (userList.Count == 0)
                {
                    Console.WriteLine($"{RED}❌ Nenhum usuário cadastrado. Cadastre um usuário primeiro!{RESET}");
                    break;
                }
                Console.WriteLine($"{CYAN}Selecione o usuário para o pedido:{RESET}");
                for (int i = 0; i < userList.Count; i++)
                    Console.WriteLine($"{YELLOW}{i + 1}. {userList[i].Name} | {userList[i].Email}{RESET}");
                int userIdx = await PromptIndex($"Digite o número do usuário (1-{userList.Count}): ", userList.Count);
                var userId = userList[userIdx - 1].Id;

                // Listar produtos e permitir múltiplos
                var productList = await client.GetFromJsonAsync<List<ProductDto>>("/api/products") ?? new List<ProductDto>();
                if (productList.Count == 0)
                {
                    Console.WriteLine($"{RED}❌ Nenhum produto cadastrado. Cadastre um produto primeiro!{RESET}");
                    break;
                }
                var orderProducts = new List<object>();
                while (true)
                {
                    Console.WriteLine($"{CYAN}Selecione um produto para adicionar ao pedido:{RESET}");
                    for (int i = 0; i < productList.Count; i++)
                        Console.WriteLine($"{YELLOW}{i + 1}. {productList[i].Name} | Estoque: {productList[i].Stock} | 💲{productList[i].Price:C}{RESET}");
                    int prodIdx = await PromptIndex($"Digite o número do produto (1-{productList.Count}): ", productList.Count);
                    var prod = productList[prodIdx - 1];
                    int qty = await PromptInt($"Quantidade para '{prod.Name}' (estoque: {prod.Stock}): ", 1, prod.Stock);
                    orderProducts.Add(new { productId = prod.Id, quantity = qty });
                    Console.Write($"Adicionar mais produtos? (s/N): ");
                    var more = Console.ReadLine();
                    if (!string.Equals(more, "s", StringComparison.OrdinalIgnoreCase))
                        break;
                }
                // Resumo
                Console.WriteLine($"{BLUE}\nResumo do pedido:{RESET}");
                foreach (dynamic item in orderProducts)
                {
                    var prod = productList.First(p => p.Id == item.productId);
                    Console.WriteLine($"{YELLOW}- {prod.Name} | Qtd: {item.quantity} | 💲{prod.Price:C}{RESET}");
                }
                Console.Write($"{CYAN}Confirmar pedido? (s/N): {RESET}");
                var confirm = Console.ReadLine();
                if (!string.Equals(confirm, "s", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"{YELLOW}❌ Pedido cancelado.{RESET}");
                    break;
                }
                var orderResp = await client.PostAsJsonAsync("/api/orders", new { userId, products = orderProducts });
                if (orderResp.IsSuccessStatusCode)
                    Console.WriteLine($"{GREEN}✅ Pedido criado com sucesso!{RESET}");
                else
                    Console.WriteLine($"{RED}❌ {await orderResp.Content.ReadAsStringAsync()}{RESET}");
                break;
            case "6":
                Console.WriteLine($"{BLUE}\n💚 Health Check:{RESET}");
                var health = await client.GetStringAsync("/api/health");
                Console.WriteLine($"{CYAN}{health}{RESET}");
                break;
            default:
                Console.WriteLine($"{RED}❌ Opção inválida!{RESET}");
                break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"{RED}Erro: {ex.Message}{RESET}");
    }
    Console.WriteLine($"{YELLOW}\nPressione ENTER para continuar...{RESET}");
    Console.ReadLine();
}

static async Task<string> Prompt(string msg)
{
    Console.Write(msg);
    return await Task.FromResult(Console.ReadLine() ?? "");
}

static async Task<int> PromptIndex(string msg, int max)
{
    int idx;
    while (true)
    {
        Console.Write(msg);
        var input = Console.ReadLine();
        if (int.TryParse(input, out idx) && idx >= 1 && idx <= max)
            return idx;
        Console.WriteLine($"Opção inválida. Digite um número entre 1 e {max}.");
    }
}

static async Task<int> PromptInt(string msg, int min, int max)
{
    int val;
    while (true)
    {
        Console.Write(msg);
        var input = Console.ReadLine();
        if (int.TryParse(input, out val) && val >= min && val <= max)
            return val;
        Console.WriteLine($"Valor inválido. Digite um número entre {min} e {max}.");
    }
}

record UserDto(Guid Id, string Name, string Email);
record ProductDto(Guid Id, string Name, string Description, decimal Price, int Stock);
