# Controle de Fluxo para Veículos
Sistema para controle do fluxo de entrada e saída de veículos por automatização.

## Ferramentas:

* Microcontrolador **Arduino** (_e componentes_) para capturar a foto da placa 
* Serviço API (_Inteligência Artificial_) para o reconhecimento da placa na imagem
* Sistema WEB em **ASP.NET Core** para o controle e análise dos dados
* Banco de dados **SQLServer** para armazenamento dos dados

Este protótipo fara uso de uma placa Arduino e do componente ESP32-CAM para obter as fotos da placa do Ônibus, após isso a imagem é enviada ao servidor e processada com o recurso de IA para reconhecimento de texto nas imagens capturadas, o servidor processa a placa e faz a tarifação automática. Além disso estará disponível análises e estatísticas de tais operações.

## API's:

#### Usuário
* ##### Add
  #### URI: "/API/User/Add" 

  Request:
  ```javascript 
      {
        Name: "Nome Exemplo",
        Registration: 123456,
        Email: "email@exemplo.com",
        Type: 1 // enum 0 = Transparência, 1 = Operador, 2 = Gerente, 3 = Administrador
      }
  ````
  Responses:


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
