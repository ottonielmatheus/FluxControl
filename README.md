# Controle de Fluxo para Veículos
Sistema para controle do fluxo de entrada e saída de veículos por automatização.

## Ferramentas:

* Microcontrolador **Arduino** (_e componentes_) para capturar a foto da placa 
* Serviço API (_Inteligência Artificial_) para o reconhecimento da placa na imagem
* Sistema WEB em **ASP.NET Core** para o controle e análise dos dados
* Banco de dados **SQLServer** para armazenamento dos dados

Este protótipo fara uso de uma placa Arduino e do componente ESP32-CAM para obter as fotos da placa do Ônibus, após isso a imagem é enviada ao servidor e processada com o recurso de IA para reconhecimento de texto nas imagens capturadas, o servidor processa a placa e faz a tarifação automática. Além disso estará disponível análises e estatísticas de tais operações.

#### Links Utéis:
* [Serviço de Email](https://app-smtp.sendinblue.com/log/#151144f3-9b77-4dfb-aed6-954ed2a1ad7b)

## API's:

#### Usuário
* ##### [POST] Add
  ###### URI: "/API/User/Add"

  #### Request:
  ```javascript 
      {
        Name: "Nome Exemplo",
        Registration: 123456,
        Email: "email@exemplo.com",
        Type: 1 // enum 0 = Transparência, 1 = Operador, 2 = Gerente, 3 = Administrador
      }
  ````
  #### Responses:
  
  Código | Retorno
  :-------:|----------
  201 | Criado com sucesso
  304 | Não criado
  424 | Falha
  
* ##### [GET] Get
  ###### URI: "/API/User/Get/{id}"

  #### Request:
  ```javascript
      "/API/User/Get/10" // URI + id do usuário
  ````
  #### Responses:
  
  Código | Retorno | Retorno 2 |
  :-------:|-------|-----------|
  200 | { User } | _null_ |
  424 | Erro ao obter usuário | |
  
* ##### [GET] Load
  ###### URI: "/API/User/Load"

  #### Request:
  ```javascript
      "/API/User/Load" // URI
  ````
  
  #### Responses:
  
  Código | Retorno | Retorno 2 |
  :-------:|-------|-----------|
  200 | [{ User }, ...] | [] |
  424 | Falha | |
  
* ##### [PATCH] Change
  ###### URI: "/API/User/Change"

  #### Request:
  ```javascript
      {
        Name: "Nome Exemplo Atualizado",
        Registration: 654321,
        Email: "email@exemplo.com",
        Type: 1 // enum 0 = Transparência, 1 = Operador, 2 = Gerente, 3 = Administrador
      }
  ````

  #### Responses:

  Código | Retorno 
  :-------:|-------
  200 | Alterado com sucesso
  304 | Não alterado
  
* ##### [POST] NewPassword
  ###### URI: "/API/User/NewPassword"

  #### Request:
  ```javascript
      "10" // id do usuário
  ````
  
  #### Responses:
  
  Código | Retorno 
  :-------:|-------
  201 | Enviado
  424 | Erro ao gerar Token
  424 | Houve um erro ao enviar o token para o email deste usuário
  
* ##### [POST] SetPassword
  ###### URI: "/API/User/SetPassword/{token}"

  #### Request:
  ```javascript
      "/API/User/SetPassword/abc23-1234-abcf-1234" // URI + token
  ````
  ```javascript
      "senha12345" // senha
  ````
  
  #### Responses:
  
  Código | Retorno 
  :-------:|-------
  201 | Senha definida
  424 | Falha ao definir senha

* ##### [DELETE] Remove
  ###### URI: "/API/User/Remove"

  #### Request:
  ```javascript
      "10" // id do usuário
  ````

  #### Responses:

  Código | Retorno 
  :-------:|-------
  200 | Removido
  424 | Falha

## Telas do Sistema:

### Operador:

Início  |
--------|
<img src="/Design/Início - Operador.png" alt="Tela de Início - Operador"/> |

Detalhes | Modal Ocorrência
---------|-----------------
<img src="/Design/Detalhes - Operador.png" alt="Detalhes da Tela - Operador"/> | <img src="/Design/Detalhes - Operador.png" alt="Detalhes da Tela - Operador"/>

### Gerente:

 Início | 
 -------|
 <img src="/Design/Início - Gerente.png" alt="Tela de Início - Gerente"/> |
 
 Relatórios |
 -----------|
 <img src="/Design/Relatórios - Gerente.png" alt="Relatórios - Gerente"/> |
 
 Pesquisa Avançada | Detalhes da Pesquisa
 ------------------|---------------------
<img src="/Design/Pesquisa Avançada Relatórios - Gerente.png" alt="Pesquisa avançada (Relatórios) - Gerente"/> | <img src="/Design/Detalhes da Pesquisa - Gerente.png" alt="Detalhes da pesquisa (Relatórios) - Gerente"/>

Cadastro funcionário |
---------------------|
<img src="/Design/Cadastro Funcionário - Gerente.png" alt="Cadastro funcionário - Gerente"/> |
<img src="/Design/Cadastro Funcionário (Tela alerta apagar funcionário) - Gerente.png" alt="Cadastro funcionário (alerta) - Gerente"/> |

Cadastro Empresa/Ônibus |
------------------------|
<img src="/Design/Cadastro Empresa-Ônibus - Gerente.png" alt="Cadastro Empresa/Ônibus - Gerente"/> |

Alerta Empresa | Alerta Ônibus
---------------|--------------
<img src="/Design/Cadastro Empresa (Tela alerta apagar empresa) - Gerente.png" alt="Cadastro Empresa/Ônibus (alerta Empresa) - Gerente"/> | <img src="/Design/Cadastro Empresa (Tela alerta apagar ônibus) - Gerente.png" alt="Cadastro Empresa/Ônibus (alerta Ônibus) - Gerente"/> 
