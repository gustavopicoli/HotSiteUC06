//Iniciando o documento e quando estiver pronto iremos carregar uma função
$(document).ready(
    function () { //Aqui temos a função que executaremos quando o documento estiver pronto
        //Verificação se meu form teve os dados enviados
        $("#frmCadastro").submit(
            function (e) {

                e.preventDefault(); //Essa função encerra o envio de dados, caso dê algum erro
                Cadastrar(); //Função que iremos chamar para que a gente envie os dados de fato

            }
        );
    }
);

function Cadastrar() {

    // array com dados do formulario
    // .val pega o valor do item
    let parametros = {
        Nome: $("#Nome").val(),
        Email: $("#Email").val(),
        Mensagem: $("#Mensagem").val(),
    }

    console.log("linha 13" + parametros);

    // requisição via POST informando o endereço e array de parametros
    $.post("/Home/Cadastrar", parametros)

        .done(
            function (data) 
            //data é referente as informações que recebemos do home/cadastrar, o arquivo JSON 
            {
                //aqui terá tratamentos
                if (data.status == "OK") {
                    $("#formulario").after("<h5>Cadastro feito com sucesso! </h5>");
                    $("#formulario").hide(); //esconder o form e colocar um aviso
                } else {
                    $("#formulario").after("<h5>" + data.mensagem + " tente mais tarde." + "</h5>");
                }
            }
        )
        .fail(
            function () {
                alert("Ocorreu um erro");
            }
        );
}

