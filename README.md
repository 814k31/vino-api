# Vino Api

# Hosted Networks
- https://vino-api.azurewebsites.net/ (Web App for Containers)
- http://20.53.92.123/ (Container Instance)

# Available Endpoints
- `/api/batches` [GET, POST]
- `/api/batches/:id` [GET, PATCH, POST, DELETE]

# Deploy
1. Build for release `dotnet publish -c Release -o ./vino-api/obj/Docker/publish`
2. Rebuild docker container `docker-compose build`
3. Deploy to dockerhub `docker push 814k31/vino-app`
