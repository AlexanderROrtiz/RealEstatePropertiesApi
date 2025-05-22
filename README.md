# RealEstate.Properties.Api

## Descripci�n

**RealEstate.Properties.Api** es una API RESTful construida siguiendo los principios de Clean Architecture y Event-Driven Architecture (EDA), orientada a la gesti�n de propiedades inmobiliarias. Permite consultar, filtrar y mantener la informaci�n de propiedades, propietarios, trazabilidad de eventos y manejo de im�genes, con persistencia en MongoDB y mensajer�a mediante Apache Kafka.

## Tecnolog�as utilizadas

- **.NET 8** (C#) � API y l�gica de negocio
- **MongoDB** � Base de datos NoSQL principal
- **Apache Kafka** � Mensajer�a para eventos de dominio
- **Confluent.Kafka** � Cliente Kafka para .NET
- **MediatR** � Implementaci�n de CQRS y mediador para desacoplamiento
- **NUnit** � Framework de pruebas unitarias
- **Swagger / Swashbuckle** � Documentaci�n interactiva de la API
- **Docker** � Contenedores para despliegue y desarrollo
- **Docker Compose** � Orquestaci�n de servicios (API, MongoDB, Kafka, Zookeeper)

## Patrones y Arquitectura

- **Clean Architecture**  
  Separaci�n por capas:  
  - Domain (entidades, l�gica de dominio, eventos)
  - Application (casos de uso, DTOs, CQRS)
  - Infrastructure (persistencia, mensajer�a, mapeos)
  - Api (controladores, endpoints, manejo de errores)

- **CQRS (Command Query Responsibility Segregation)**  
  Separaci�n clara entre comandos (mutaciones) y consultas (queries).

- **Event Driven Architecture (EDA)**  
  Integraci�n con Kafka para la publicaci�n y posible consumo de eventos de dominio como la creaci�n, actualizaci�n y eliminaci�n de propiedades.

- **Repository Pattern**  
  Para el acceso abstracto a la persistencia en MongoDB.

## C�mo clonar y correr el proyecto

### 1. Clonar el repositorio

git clone https://github.com/AlexanderROrtiz/RealEstatePropertiesApi.git

cd RealEstate.Properties.Api

### 2. Configurar variables de entorno

Revisa y ajusta los archivos de configuraci�n en `appsettings.json` y/o variables de entorno para la conexi�n a MongoDB y Kafka si es necesario.

### 3. Construir y levantar con Docker Compose

Aseg�rate de tener Docker y Docker Compose instalados.

docker-compose build
docker-compose up

Esto levantar�:
- La API en .NET 8
- MongoDB
- Apache Kafka (con Zookeeper)

La API estar� accesible en [http://localhost:5000/swagger](http://localhost:5000/swagger) (o el puerto que definas).

### 4. Correr pruebas unitarias

Pruebas unitarias m�nimas para Application:

- GetPropertiesQueryHandlerTests
- CreatePropertyCommandHandlerTests

dotnet test

**Nota:**  
Toda la l�gica de negocio reside en Application, por lo que las pruebas unitarias se enfocan en esa capa.

## Endpoints principales

- GET /api/properties � Lista de propiedades con filtros por nombre, direcci�n y rango de precios.  
  **Ejemplo de filtro:**  

- GET /api/properties?name=Casa&address=Bogot�&minPrice=100000000&maxPrice=300000000

- GET /api/properties/{id} � Detalle de una propiedad

- POST /api/properties � Crear una nueva propiedad

- POST /api/owners � Crear un nuevo propietario

Otros endpoints disponibles en la documentaci�n Swagger.


### Ejemplo: Crear una propiedad

**Request:**

POST /api/properties

**Body:**

json
{
  "id": "property7",
  "name": "Casa Bonita",
  "address": "Calle 123, Bogot�",
  "price": 250000000,
  "codeInternal": "CB-001",
  "year": 2015,
  "ownerId": "owner1",
  "ownerName": "juan",
  "imageUrl": "https://images.unsplash.com/photo-1506744038136-46273834b3fb"
}

**Respuesta esperada:**

json
{
  "message": "Propiedad creada correctamente.",
  "data": {
    "id": "property7",
    "name": "Casa Bonita",
    "address": "Calle 123, Bogot�",
    "price": 250000000,
    "codeInternal": "CB-001",
    "year": 2015,
    "ownerId": "owner1",
    "ownerName": "juan",
    "imageUrl": "https://images.unsplash.com/photo-1506744038136-46273834b3fb"
  }
}

### Ejemplo: Crear un propietario

**Request:**

POST /api/owners

**Body:**

json
{
  "idOwner": "owner1",
  "name": "juan",
  "address": "Calle 45, Bogot�",
  "photo": "https://images.unsplash.com/photo-1506744038136-46273834b3fb",
  "birthday": "2025-05-21T23:46:39.112Z"
}


**Respuesta esperada:**

json
{
  "message": "Propietario creado correctamente.",
  "data": {
    "idOwner": "owner1",
    "name": "juan",
    "address": "Calle 45, Bogot�",
    "photo": "https://images.unsplash.com/photo-1506744038136-46273834b3fb",
    "birthday": "2025-05-21T23:46:39.112Z"
  }
}

## Notas y buenas pr�cticas

- Sigue la estructura de carpetas propuesta para mantener el c�digo desacoplado y escalable.
- Se recomienda agregar variables de entorno en un archivo `.env` para configuraci�n sensible.
- La comunicaci�n con Kafka est� desacoplada, permitiendo a�adir microservicios consumidores en el futuro.
- Se agregaron solo pruebas de ejemplo para la capa de RealEstate.Properties.Application.Tests
- Para probar el flujo de **kafka** se detona la configuracion solo para el comando de crear propiedad **CreatePropertyCommandHandler**, pero se puede extender a otros eventos.
- Swagger est� habilitado para explorar y probar los endpoints de forma interactiva.
- Los filtros para b�squeda de propiedades pueden combinarse (nombre, direcci�n, rango de precios).
- El c�digo sigue las convenciones y buenas pr�cticas de C# y .NET para garantizar mantenibilidad y escalabilidad.


## Kafka: Ejemplo de evento publicado con la logica implementada en **CreatePropertyCommandHandler**

Al crear una propiedad, se publica autom�ticamente un evento en Kafka (topic: `property-created`).  
**Ejemplo de mensaje:**

json

Mensaje publicado en el t�pico de Kafka: 'property-created': 
{
     "Property":
     {
        "IdProperty":"property7",
        "Name":"Casa Bonita",
        "Address":"Calle 123, Bogot\u00E1",
        "Price":250000000,
        "CodeInternal":"CB-001",
        "Year":2015,
        "IdOwner":"owner1",
        "Owner":
          {
          "IdOwner":"owner1",
          "Name":"juan",
          "Address":"Calle 123, Bogot\u00E1 dddd",
          "Photo":"https://randomuser.me/api/portraits/men/2.jpg",
          "Birthday":"1995-04-23T05:00:00Z"
          },
        "Images":
        [
            {
                "IdPropertyImage":null,
                "IdProperty":null,
                "File":"https://images.unsplash.com/photo-1506744038136-46273834b3fb",
                "Enabled":true
            }
        ],
        "Traces":[]
     },
        "OccurredOn":"2025-05-22T02:26:45.4059232Z"
}


## Contacto y soporte

Para m�s informaci�n o sugerencias de mejora:

LinkedIn: [https://www.linkedin.com/in/roberth-ortiz-b526331a9/](https://www.linkedin.com/in/roberth-ortiz-b526331a9/)

�Gracias por tu inter�s!