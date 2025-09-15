#!/bin/bash

# =============================
# Configuração
# =============================
BASE_URL="http://localhost:5147"
export LANG=C.UTF-8

echo "🚀 Testando API Completa - $BASE_URL"
echo "============================================="

# =============================
# Função para requisições
# =============================
make_request() {
    local method="$1"
    local url="$2"
    local data="$3"
    local description="$4"

    echo -e "\n${description}"
    echo "Método: $method"
    echo "URL: $url"
    if [ ! -z "$data" ]; then
        echo "Dados: $data"
    fi
    echo "Resposta:"
    echo "----------------------------------------"

    if [ ! -z "$data" ]; then
        response=$(curl -s -w "\nStatus HTTP: %{http_code}\nTempo: %{time_total}s\n" \
            -X "$method" "$url" \
            -H "Content-Type: application/json; charset=utf-8" \
            -H "Accept: application/json; charset=utf-8" \
            --data-raw "$data")
    else
        response=$(curl -s -w "\nStatus HTTP: %{http_code}\nTempo: %{time_total}s\n" \
            -X "$method" "$url" \
            -H "Accept: application/json; charset=utf-8")
    fi

    # Formata JSON se possível
    if command -v jq > /dev/null 2>&1; then
        echo "$response" | head -n -2 | jq . 2>/dev/null || echo "$response" | head -n -2
    else
        echo "$response" | head -n -2
    fi

    # Exibe status HTTP e tempo
    echo "$response" | tail -n 2
    echo "----------------------------------------"
}

# =============================
# TESTES MOTO
# =============================
echo -e "\n============================================="
echo "🧑‍🏍 TESTANDO ENDPOINTS DE MOTOS"
echo "============================================="

# 1. CRIAR MOTO
make_request "POST" "$BASE_URL/motos" \
'{
    "placaMoto": "ABC1235",
    "modeloMoto": "Mottu Sport",
    "situacaoMoto": "Ativa",
    "chassiMoto": "9C2JC5020NR123456"
}' \
"📝 1. CRIAR MOTO"

# 2. LISTAR MOTOS
make_request "GET" "$BASE_URL/motos?pageNumber=1&pageSize=10" "" \
"📋 2. LISTAR MOTOS"

# 3. BUSCAR POR ID
make_request "GET" "$BASE_URL/motos/33" "" \
"🔍 3. BUSCAR MOTO POR ID (33)"

# 4. BUSCAR POR CHASSI
make_request "GET" "$BASE_URL/motos/por-chassi/CHS90000000000025" "" \
"🏍️ 4. BUSCAR MOTO POR CHASSI"

# 5. ATUALIZAR MOTO
make_request "PUT" "$BASE_URL/motos/33" \
'{
    "placaMoto": "ABC1236",
    "modeloMoto": "Mottu Pop",
    "situacaoMoto": "Ativa",
    "chassiMoto": "9C2JC5020NR123457"
}' \
"✏️ 5. ATUALIZAR MOTO (ID 33)"

# 6. ASSOCIAR CLIENTE
make_request "PUT" "$BASE_URL/motos/33/alterar-cliente/2" "" \
"👤 6. ASSOCIAR CLIENTE 2 À MOTO 33"

# 7. REMOVER CLIENTE
make_request "PUT" "$BASE_URL/motos/33/remover-cliente" "" \
"❌ 7. REMOVER CLIENTE DA MOTO 33"

# 8. DELETAR MOTO
make_request "DELETE" "$BASE_URL/motos/33" "" \
"🗑️ 8. DELETAR MOTO (ID 33)"

# =============================
# TESTES CLIENTE
# =============================
echo -e "\n============================================="
echo "🧑‍💼 TESTANDO ENDPOINTS DE CLIENTES"
echo "============================================="

# 1. CRIAR CLIENTE
make_request "POST" "$BASE_URL/clientes" \
'{
    "nomeCliente": "Carlos dos Santos",
    "telefoneCliente": "11999999999",
    "sexoCliente": "M",
    "emailCliente": "joao.santos@email.com",
    "cpfCliente": "12345678909"
}' \
"👤 1. CRIAR CLIENTE"

# 2. LISTAR CLIENTES
make_request "GET" "$BASE_URL/clientes?pageNumber=1&pageSize=10" "" \
"📋 2. LISTAR CLIENTES"

# 3. BUSCAR CLIENTE POR ID
make_request "GET" "$BASE_URL/clientes/32" "" \
"🔍 3. BUSCAR CLIENTE POR ID (32)"

# 4. ATUALIZAR CLIENTE
make_request "PUT" "$BASE_URL/clientes/32" \
'{
    "nomeCliente": "Jonas dos Santos Jr",
    "telefoneCliente": "11999999998",
    "sexoCliente": "M",
    "emailCliente": "jonas.santosjr@email.com",
    "cpfCliente": "12345678906"
}' \
"✏️ 4. ATUALIZAR CLIENTE (ID 32)"

# 5. DELETAR CLIENTE
make_request "DELETE" "$BASE_URL/clientes/32" "" \
"🗑️ 5. DELETAR CLIENTE (ID 32)"

# =============================
# TESTES MOVIMENTAÇÕES
# =============================
echo -e "\n============================================="
echo "📦 TESTANDO ENDPOINTS DE MOVIMENTAÇÕES"
echo "============================================="

# 1. CRIAR MOVIMENTAÇÃO
make_request "POST" "$BASE_URL/movimentacoes" \
'{
    "descricaoMovimentacao": "Entrada da moto 5 na vaga 5",
    "motoId": 5,
    "vagaId": 5
}' \
"📝 1. CRIAR MOVIMENTAÇÃO"

# 2. LISTAR MOVIMENTAÇÕES
make_request "GET" "$BASE_URL/movimentacoes?pageNumber=1&pageSize=10" "" \
"📋 2. LISTAR MOVIMENTAÇÕES"

# 3. BUSCAR MOVIMENTAÇÃO POR ID
make_request "GET" "$BASE_URL/movimentacoes/1" "" \
"🔍 3. BUSCAR MOVIMENTAÇÃO POR ID (1)"

# 4. BUSCAR MOVIMENTAÇÕES POR MOTO
make_request "GET" "$BASE_URL/movimentacoes/por-moto/2?pageNumber=1&pageSize=10" "" \
"🏍️ 4. BUSCAR MOVIMENTAÇÕES POR MOTO (ID 2)"

# 5. OCUPAÇÃO POR SETOR DE PÁTIO
make_request "GET" "$BASE_URL/movimentacoes/ocupacao-por-setor/patio/1?pageNumber=1&pageSize=10" "" \
"🏢 5. OCUPAÇÃO POR SETOR DO PÁTIO (ID 1)"

# 6. REGISTRAR SAÍDA
make_request "PUT" "$BASE_URL/movimentacoes/3/saida" "" \
"🚪 6. REGISTRAR SAÍDA DA MOVIMENTAÇÃO (ID 3)"

echo -e "\n✅ Todos os testes concluídos!"
echo "============================================="
echo "📊 RESUMO DOS TESTES:"
echo "• ✅ Motos: 8 endpoints testados"
echo "• ✅ Clientes: 5 endpoints testados" 
echo "• ✅ Movimentações: 6 endpoints testados"
echo "• 📈 Total: 19 endpoints testados"
echo "============================================="

