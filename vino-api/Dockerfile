FROM mcr.microsoft.com/dotnet/aspnet:3.1
ARG source
RUN echo "${source}"
WORKDIR /app
EXPOSE 80
COPY ${source:-obj/Docker/publish} .
ENTRYPOINT ["dotnet", "vino-api.dll"]
