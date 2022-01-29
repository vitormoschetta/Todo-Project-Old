### Add Migrations
```
cd ./src
dotnet ef migrations add initial -o Data/migrations
dotnet ef database update
```


### Up Local Containers:
```
docker-compose up -d --build
```


### Build Image for Production

Update `appsettings.json` file:
```
"Urls": "http://localhost:80"

"DefaultConnection": "your connection for database"
```

Build image:
```
docker build -t vitormoschetta/todoapi -f Dockerfile .
```

Run image in Server:
```
sudo docker run -d --name todoapi.app --network host -d vitormoschetta/todoapi
```