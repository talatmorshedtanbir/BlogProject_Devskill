FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["BlogProject_Devskill.Web/*.csproj", "BlogProject_Devskill.Web/"]
COPY ["BlogProject_Devskill.Framework/*.csproj", "BlogProject_Devskill.Framework/"]
COPY ["BlogProject_Devskill.Data/*.csproj", "BlogProject_Devskill.Data/"]
COPY ["BlogProject_Devskill.Membership/*.csproj", "BlogProject_Devskill.Membership/"]
COPY ["BlogProject_Devskill.Framework.Tests/*.csproj", "BlogProject_Devskill.Framework.Tests/"]
RUN dotnet restore "BlogProject_Devskill.Web/BlogProject_Devskill.Web.csproj"
COPY . .
WORKDIR "/src/BlogProject_Devskill.Web"
RUN dotnet build "BlogProject_Devskill.Web.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "BlogProject_Devskill.Web.csproj" -c Release -o /app

FROM build AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "BlogProject_Devskill.Web.dll"]