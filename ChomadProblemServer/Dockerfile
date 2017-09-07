FROM microsoft/aspnetcore:1.1
ARG source
WORKDIR /app
EXPOSE 80
COPY ${source:-obj/Docker/publish} .
ENV PORT 80
CMD ASPNETCORE_URLS=http://*:$PORT dotnet ChomadProblemServer.dll
