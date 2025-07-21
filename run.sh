#!/bin/bash

# Cores para output
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
BLUE='\033[0;34m'
NC='\033[0m' # No Color

show_help() {
    echo -e "${BLUE}📖 Uso do script:${NC}"
    echo -e "  ${GREEN}./run.sh${NC}                    - Build, migrate e executa a API"
    echo -e "  ${GREEN}./run.sh build${NC}              - Apenas dotnet build"
    echo -e "  ${GREEN}./run.sh restore${NC}            - Apenas dotnet restore"
    echo -e "  ${GREEN}./run.sh clean${NC}              - Apenas dotnet clean"
    echo -e "  ${GREEN}./run.sh test${NC}               - Executa testes (se existirem)"
    echo -e "  ${GREEN}./run.sh migrate${NC}            - Executa migrations"
    echo -e "  ${GREEN}./run.sh run${NC}                - Apenas executa a API"
    echo -e "  ${GREEN}./run.sh watch${NC}              - Executa dotnet watch run na API"
    echo -e "  ${GREEN}./run.sh help${NC}               - Mostra esta ajuda"
    echo ""
    echo -e "${YELLOW}💡 Exemplos:${NC}"
    echo -e "  ${GREEN}./run.sh build${NC}              - Para fazer apenas o build"
}

check_dotnet() {
    if ! dotnet --version > /dev/null 2>&1; then
        echo -e "${RED}❌ .NET 8 não está instalado. Por favor, instale o .NET 8 e tente novamente.${NC}"
        exit 1
    fi
}

run_migrations() {
    echo -e "${YELLOW}🗄️ Executando migrations...${NC}"
    cd Ecommerce.WebApi
    dotnet ef database update
    cd ..
}

run_api() {
    echo -e "${GREEN}🎯 Iniciando a API...${NC}"
    echo -e "${BLUE}📱 A API estará disponível em: http://localhost:5000${NC}"
    echo -e "${BLUE}📚 Swagger estará disponível em: http://localhost:5000/swagger${NC}"
    echo -e "${YELLOW}⏹️ Pressione Ctrl+C para parar${NC}"
    cd Ecommerce.WebApi
    dotnet run
}

case "${1:-}" in
    "build")
        echo -e "${BLUE}🔨 Executando dotnet build...${NC}"
        check_dotnet
        dotnet build
        ;;
    "restore")
        echo -e "${BLUE}📦 Executando dotnet restore...${NC}"
        check_dotnet
        dotnet restore
        ;;
    "clean")
        echo -e "${BLUE}🧹 Executando dotnet clean...${NC}"
        check_dotnet
        dotnet clean
        ;;
    "test")
        echo -e "${BLUE}🧪 Executando testes...${NC}"
        check_dotnet
        dotnet test
        ;;
    "migrate")
        echo -e "${BLUE}🗄️ Executando migrations...${NC}"
        check_dotnet
        run_migrations
        ;;
    "run")
        echo -e "${BLUE}🎯 Executando API...${NC}"
        check_dotnet
        run_api
        ;;
    "watch")
        echo -e "${BLUE}👀 Executando dotnet watch run na API...${NC}"
        check_dotnet
        cd Ecommerce.WebApi
        dotnet watch run
        ;;
    "help"|"-h"|"--help")
        show_help
        ;;
    "")
        echo -e "${BLUE}🚀 Iniciando Ecommerce Clean Architecture...${NC}"
        check_dotnet
        echo -e "${GREEN}✅ .NET 8 verificado${NC}"
        echo -e "${YELLOW}📦 Restaurando pacotes NuGet...${NC}"
        dotnet restore
        echo -e "${YELLOW}🔨 Fazendo build da aplicação...${NC}"
        if ! dotnet build; then
            echo -e "${RED}❌ Build falhou${NC}"
            exit 1
        fi
        echo -e "${GREEN}✅ Build concluído com sucesso!${NC}"
        run_migrations
        run_api
        ;;
    *)
        echo -e "${RED}❌ Comando desconhecido: $1${NC}"
        echo ""
        show_help
        exit 1
        ;;
esac 