# Controle de Fluxo para Veículos
Sistema para controle do fluxo de entrada e saída de veículos por automatização.

Ferramentas:

* Microcontrolador para capturar a foto da placa 
* Serviço API (_Inteligência Artificial_) para o reconhecimento da placa na imagem
* Sistema WEB em ASP.NET Core para o controle e análise dos dados


Este protótipo fara uso de uma placa Arduino e do componente ESP32-CAM para obter as fotos da placa do Ônibus, após isso a imagem é enviada ao servidor e processada com o recurso de IA para reconhecimento de texto nas imagens capturadas, o servidor processa a placa e faz a tarifação automática. Além disso estará disponível análises e estatísticas de tais operações.

<img src="/Design/Início - Operador.png" alt="Tela de Início - Operador"/>
