FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/WiseReminder.WebAPI/WiseReminder.WebAPI.csproj", "src/WiseReminder.WebAPI/"]
COPY ["src/WiseReminder.Domain/WiseReminder.Domain.csproj", "src/WiseReminder.Domain/"]
COPY ["src/WiseReminder.Application/WiseReminder.Application.csproj", "src/WiseReminder.Application/"]
COPY ["src/WiseReminder.Infrastructure/WiseReminder.Infrastructure.csproj", "src/WiseReminder.Infrastructure/"]
RUN dotnet restore "src/WiseReminder.WebAPI/WiseReminder.WebAPI.csproj"
COPY . .
WORKDIR "/src/src/WiseReminder.WebAPI"
RUN dotnet build "WiseReminder.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "WiseReminder.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV DOTNET_RUNNING_IN_CONTAINER=true
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "WiseReminder.WebAPI.dll"]
