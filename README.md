# API Wrapper to Integrate Azure OpeAI ChatGPT model with your own data over API


Currently, you have two options to seamlessly integrate the Azure OpenAI ChatGPT model with your own data in external applications: AI Studio (Deploy App) and REST API. The provided code exposes a REST API over Azure Function, making it easier to leverage the capabilities of ChatGPT within your applications. By grounding the model with your own data, you unlock its full potential and enable the delivery of dynamic conversational experiences through a convenient API interface. This code empowers you to tap into the rich features of ChatGPT and effortlessly deliver exceptional user interactions.


More info and param configurtation: https://learn.microsoft.com/en-us/azure/cognitive-services/openai/use-your-data-quickstart?tabs=command-line&pivots=rest-api

```AOAIEndpoint``` -	This value can be found in the Keys & Endpoint section when examining your Azure OpenAI resource from the Azure portal. Alternatively, you can find the value in Azure AI studio > Chat playground > Code view. An example endpoint is: https://my-resoruce.openai.azure.com.<br>
```#AOAIKey```	- This value can be found in the Keys & Endpoint section when examining your Azure OpenAI resource from the Azure portal. You can use either KEY1 or KEY2. Always having two keys allows you to securely rotate and regenerate keys without causing a service disruption.<br>
```#AOAIDeploymentId``` -	This value corresponds to the custom name you chose for your deployment when you deployed a model. This value can be found under Resource Management > Deployments in the Azure portal or alternatively under Management > Deployments in Azure AI studio.<br>
```#ChatGptUrl```	- The Azure OpenAI ChatGPT endpoint that will be used to fulfill the request. This can be the same endpoint as AoAIEndpoint.<br>
```#ChatGptKey```	- If you are using the same Azure OpenAI resource for both ChatGptUrl and AOAIEndpoint, use the same value as AOAIKey.<br>
```#SearchEndpoint```	- This value can be found in the Keys & Endpoint section when examining your Azure Cognitive Search resource from the Azure portal.<br>
```#SearchKey```	- This value can be found in the Keys & Endpoint section when examining your Azure Cognitive Search resource from the Azure portal. You can use either KEY1 or KEY2. Always having two keys allows you to securely rotate and regenerate keys without causing a service disruption.<br>
```#SearchIndex```	- This value corresponds to the name of the index you created to store your data. You can find it in the Overview section when examining your Azure Cognitive Search resource from the Azure portal.<br>
