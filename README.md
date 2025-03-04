# Microservices.AspNetCore

## Overview

A simple example project on how to create and communicate with microservices using AspNetCore.

## Technologies

- AspNetCore 9.0
- Docker
- Docker Compose
- Apache Kafka (Confluent)
- Entity Framework Core
- PostgreSQL

## Setup

1. Clone the repository & install the dependencies:

```bash
git clone https://github.com/dagweg/Microservices.AspNetCore.git
dotnet restore
pnpm install .\microservices.client
```

2. Make sure you have [Docker Desktop](https://www.docker.com/products/docker-desktop/) installed.
3. Run Zookeeper and Kafka using Docker Compose on the file `kafka.yml`:

```bash
docker-compose -f .\Microservices.AspNetCore.AppHost\kafka.yml up -d
```

4. While this is running, run the Aspire API project:

```bash
dotnet watch run --project .\Microservices.AspNetCore.AppHost\
```

5. Run the Next.Js Client

```bash
pnpm dev .\microservices.client
```

Test the project by placing product orders and observing the value after a few seconds.

## How it works

1. Input a quantity and place an order.
![image](https://github.com/user-attachments/assets/e5a849d2-9790-42ad-aac5-43143fbaa1bc)
2. This makes a POST request to the API, which publishes a message to the Kafka Broker.
![image](https://github.com/user-attachments/assets/f0a50f60-9f24-4d54-a2b4-424faf6c865d)
3. The **Product-Microservice** has event-handlers running in the background that continually listen to `OrderPlacedEvent`. When it receives an event, it checks the stock quantity and updates the stock. Otherwise it produces an 'OrderRejectedEvent'.
![image](https://github.com/user-attachments/assets/3e75747c-f69f-4158-8ee2-3ddc39621cc2)
![image](https://github.com/user-attachments/assets/747ea1e3-464a-47d9-858a-f2823894a81a)
5. The **Order-Microservice** listens to either 'OrderCreatedEvent' or 'OrderRejectedEvent' and updates the order status accordingly.
![image](https://github.com/user-attachments/assets/e453d45a-e058-49b3-9b90-7b8ee03c223a)
6. Finally, the client updates the new stock.
![image](https://github.com/user-attachments/assets/7ab224a0-a1ec-4538-ba64-2d8f4223d5a5)





