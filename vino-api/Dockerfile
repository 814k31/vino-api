FROM mcr.microsoft.com/dotnet/aspnet
ARG source
WORKDIR /app
EXPOSE 80
COPY ${source:-obj/Docker/publish} .
ENTRYPOINT ["dotnet", "vino-api.dll"]
