#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

COPY ["QuestionService", "QuestionService/"]
COPY ["Atos.Core", "Atos.Core/"]

RUN dotnet restore "QuestionService/src/QuestionServiceAPI/QuestionServiceAPI.csproj"

COPY QuestionService/ .
COPY Atos.Core/ .

WORKDIR "/src/QuestionService/src/QuestionServiceAPI"

RUN dotnet build "QuestionServiceAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "QuestionServiceAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app

ARG dbpass
ARG dbuser
ARG dbhost
ARG dbport
ARG dbname

ARG mqhost
ARG mqport
ARG mquser
ARG mqpass

ENV MQUSER=$mquser
ENV MQPASSWORD=$mqpassword
ENV MQHOST=$mqhost
ENV MQPORT=$port

ENV DBPASSWORD=$dbpass
ENV DBUSER=$dbuser
ENV DBHOST=$dbhost
ENV DBPORT=$dbport
ENV DBNAME=$dbname

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "QuestionServiceAPI.dll"]
