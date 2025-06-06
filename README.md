# GraphRAG LLM API

## Overview

The GraphRAG LLM API is a .NET Core application that implements a Graph-Based Retrieval Augmented Generation (GraphRAG) system. It utilizes Semantic Kernel and Re-Ranking techniques to enhance the generation of responses from a Large Language Model (LLM). The architecture follows Clean Architecture principles and employs the CQRS pattern with MediatR for command and query handling.

## Features

- **Graph-Based Retrieval**: Efficiently retrieves relevant documents and their connections using a graph structure.
- **Retrieval Augmented Generation**: Combines retrieval and generation processes to produce contextually relevant responses.
- **Dependency Injection**: Utilizes built-in dependency injection for managing service lifetimes and dependencies.
- **Vector Database**: Integrates with PostgreSQL using pgvector for storing and retrieving document embeddings.
- **LLM Integration**: Communicates with the LLM service via Docker, specifically using Ollama.
- **Testing**: Includes unit tests and integration tests to ensure the reliability of the application.

## Project Structure

- **Domain Layer**: Contains entities, interfaces, and value objects that define the core business logic.
- **Application Layer**: Implements commands, queries, and services that orchestrate the application's behavior.
- **Infrastructure Layer**: Provides implementations for data access, LLM services, and dependency injection.
- **Web API Layer**: Exposes HTTP endpoints for interacting with the application.

## Setup

### Prerequisites

- .NET 6.0 or later
- PostgreSQL with pgvector extension
- Docker and Docker Compose

### Running the Application

1. Clone the repository:
   ```
   git clone <repository-url>
   cd GraphRAGLlmApi
   ```

2. Set up the PostgreSQL database and run the Docker containers:
   ```
   docker-compose up -d
   ```

3. Run the application:
   ```
   dotnet run --project src/GraphRAGLlmApi.WebApi/GraphRAGLlmApi.WebApi.csproj
   ```

### Testing

To run the unit and integration tests, use the following command:
```
dotnet test
```

### HTTP Requests

You can test the API using the provided `query.http` file located in `src/GraphRAGLlmApi.WebApi/Requests/`. This file contains example HTTP requests for interacting with the API.

## Contributing

Contributions are welcome! Please feel free to submit a pull request or open an issue for any enhancements or bug fixes.

## License

This project is licensed under the MIT License. See the LICENSE file for more details.