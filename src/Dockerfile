FROM mcr.microsoft.com/dotnet/aspnet:8.0 as base
WORKDIR /app
EXPOSE 80
EXPOSE 443


FROM mcr.microsoft.com/dotnet/sdk:8.0 as build
ARG MICROSVC
ARG CSPROJ
WORKDIR /src
COPY . .
RUN dotnet restore ${MICROSVC}/${CSPROJ}
WORKDIR /src/${MICROSVC}
RUN dotnet build ${CSPROJ} -c Release -o /app/build --verbosity minimal


FROM build as publish
RUN dotnet publish ${CSPROJ} -c Release -o /app/publish --verbosity minimal

FROM base as final
ARG ASSEMBLY
WORKDIR /app
COPY --from=publish /app/publish .
RUN echo "#!/bin/bash" > ./entrypoint.sh
RUN echo "dotnet ${ASSEMBLY}" >> ./entrypoint.sh
RUN chmod +x ./entrypoint.sh

#env variables to set in order to test the docker container locally
ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_URLS=http://+:80
ENV DatabaseSettings__ConnectionString=Host=host.docker.internal;Username=postgres;Password=postgres;Database=ouijjane-local-village;Port=5432
ENV DatabaseSettings__DBProvider=postgresql
ENV MicroService__Product=ouijjane
ENV MicroService__Module=village
ENV MicroService__Component=api
ENV MicroService__Version=1.0.0
ENV MicroService__Namespace=local
#docker build . -t xxx-my_tag-xxx --build-arg MICROSVC=Ouijjane.Village.Api --build-arg CSPROJ=Ouijjane.Village.Api.csproj --build-arg ASSEMBLY=Ouijjane.Village.Api.dll
#docker run --name village-api-local -p 5001:80 -d xxx-my_tag-xxx    (http://localhost:5001/api/inhabitants)

ENTRYPOINT ["./entrypoint.sh"]


