# RealEstate.Properties.Api

## Descripción

**RealEstate.Properties.Api** es una API RESTful construida siguiendo los principios de Clean Architecture y Event-Driven Architecture (EDA), orientada a la gestión de propiedades inmobiliarias. Permite consultar, filtrar y mantener la información de propiedades, propietarios, trazabilidad de eventos y manejo de imágenes, con persistencia en MongoDB y mensajería mediante Apache Kafka.

## Tecnologías utilizadas

- **.NET 8** (C#) — API y lógica de negocio
- **MongoDB** — Base de datos NoSQL principal
- **Apache Kafka** — Mensajería para eventos de dominio
- **Confluent.Kafka** — Cliente Kafka para .NET
- **MediatR** — Implementación de CQRS y mediador para desacoplamiento
- **NUnit** — Framework de pruebas unitarias
- **Swagger / Swashbuckle** — Documentación interactiva de la API
- **Docker** — Contenedores para despliegue y desarrollo
- **Docker Compose** — Orquestación de servicios (API, MongoDB, Kafka, Zookeeper)

## Patrones y Arquitectura

- **Clean Architecture**  
  Separación por capas:  
  - Domain (entidades, lógica de dominio, eventos)
  - Application (casos de uso, DTOs, CQRS)
  - Infrastructure (persistencia, mensajería, mapeos)
  - Api (controladores, endpoints, manejo de errores)

- **CQRS (Command Query Responsibility Segregation)**  
  Separación clara entre comandos (mutaciones) y consultas (queries).

- **Event Driven Architecture (EDA)**  
  Integración con Kafka para la publicación y posible consumo de eventos de dominio como la creación, actualización y eliminación de propiedades.

- **Repository Pattern**  
  Para el acceso abstracto a la persistencia en MongoDB.

## Cómo clonar y correr el proyecto

### 1. Clonar el repositorio

git clone https://github.com/AlexanderROrtiz/RealEstatePropertiesApi.git

cd RealEstate.Properties.Api

### 2. Configurar variables de entorno

Revisa y ajusta los archivos de configuración en `appsettings.json` y/o variables de entorno para la conexión a MongoDB y Kafka si es necesario.

### 3. Construir y levantar con Docker Compose

Asegúrate de tener Docker y Docker Compose instalados.

docker-compose build
docker-compose up

Esto levantará:
- La API en .NET 8
- MongoDB
- Apache Kafka (con Zookeeper)

La API estará accesible en [http://localhost:5000/swagger](http://localhost:5000/swagger) (o el puerto que definas).

### 4. Correr pruebas unitarias

Pruebas unitarias mínimas para Application:

- GetPropertiesQueryHandlerTests
- CreatePropertyCommandHandlerTests

dotnet test

**Nota:**  
Toda la lógica de negocio reside en Application, por lo que las pruebas unitarias se enfocan en esa capa.

## Endpoints principales

- GET /api/properties — Lista de propiedades con filtros por nombre, dirección y rango de precios.  
  **Ejemplo de filtro:**  

- GET /api/properties?name=Casa&address=Bogotá&minPrice=100000000&maxPrice=300000000

- GET /api/properties/{id} — Detalle de una propiedad

- POST /api/properties — Crear una nueva propiedad

- POST /api/owners — Crear un nuevo propietario

Otros endpoints disponibles en la documentación Swagger.


### Ejemplo: Crear una propiedad

**Request:**

POST /api/properties

**Body:**

json
{
  "id": "property7",
  "name": "Casa Bonita",
  "address": "Calle 123, Bogotá",
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
    "address": "Calle 123, Bogotá",
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
  "address": "Calle 45, Bogotá",
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
    "address": "Calle 45, Bogotá",
    "photo": "https://images.unsplash.com/photo-1506744038136-46273834b3fb",
    "birthday": "2025-05-21T23:46:39.112Z"
  }
}

## Notas y buenas prácticas

- Sigue la estructura de carpetas propuesta para mantener el código desacoplado y escalable.
- Se recomienda agregar variables de entorno en un archivo `.env` para configuración sensible.
- La comunicación con Kafka está desacoplada, permitiendo añadir microservicios consumidores en el futuro.
- Se agregaron solo pruebas de ejemplo para la capa de RealEstate.Properties.Application.Tests
- Para probar el flujo de **kafka** se detona la configuracion solo para el comando de crear propiedad **CreatePropertyCommandHandler**, pero se puede extender a otros eventos.
- Swagger está habilitado para explorar y probar los endpoints de forma interactiva.
- Los filtros para búsqueda de propiedades pueden combinarse (nombre, dirección, rango de precios).
- El código sigue las convenciones y buenas prácticas de C# y .NET para garantizar mantenibilidad y escalabilidad.


## Kafka: Ejemplo de evento publicado con la logica implementada en **CreatePropertyCommandHandler**

Al crear una propiedad, se publica automáticamente un evento en Kafka (topic: `property-created`).  
**Ejemplo de mensaje:**

json

Mensaje publicado en el tópico de Kafka: 'property-created': 
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

Para más información o sugerencias de mejora:

LinkedIn: [https://www.linkedin.com/in/roberth-ortiz-b526331a9/](https://www.linkedin.com/in/roberth-ortiz-b526331a9/)

¡Gracias por tu interés!