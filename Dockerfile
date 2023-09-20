FROM mcr.microsoft.com/dotnet/sdk:8.0 as base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/asp:8.0 as build
ARG MICROSVC
ARG CSRPOJ
WORKDIR /src
COPY . .
WORKDIR ${MICROSVC}
RUN dotnet restore ${CSPROJ}
RUN dotnet build ${CSPROJ} -c Release -o /app/build --verbosity minimal

FROM build as publish
RUN dotnet publish ${CSPROJ} -c Release -o /app/publish --verbosity minimal

FROM base as final
ARG ASSEMBLY
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "${ASSEMBLY}"]
