FROM mcr.microsoft.com/dotnet/sdk:5.0 as build-image
WORKDIR /home/app
COPY ./*.sln ./
COPY ./*/*.csproj ./
RUN for file in $(ls *.csproj); do mkdir -p ./${file%.*}/ && mv $file ./${file%.*}/; done
RUN dotnet restore
COPY . .
RUN dotnet dev-certs https
RUN dotnet dev-certs https --trust
RUN dotnet test ./EmotionalCalendar.Backend.WebAPI.Tests/EmotionalCalendar.Backend.WebAPI.Tests.csproj
RUN dotnet publish ./EmotionalCalendar.Backend.WebAPI/EmotionalCalendar.Backend.WebAPI.csproj -o /publish/
WORKDIR /publish
COPY --from=build-image /publish .
ENV ASPNETCORE_URLS=https://+:5001;http://+:5000
ENTRYPOINT ["dotnet", "EmotionalCalendar.Backend.WebAPI.dll"]
