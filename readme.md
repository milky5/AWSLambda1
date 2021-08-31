# memo

`sln`があるディレクトリで下記コマンドを実行

```sh
# dotnet publish --framework netcoreapp3.1 --runtime linux-x64
dotnet publish --framework netcoreapp3.1 --runtime rhel-x64
```

`Dockerfile`記載後、このディレクトリで下記コマンドを実行

```sh
docker build -t mylambda .
```

```sh
docker run -p 9000:8080 mylambda
```

```sh
curl -XPOST "http://localhost:9000/2015-03-31/functions/function/invocations" -d '{"payload":"hello world!"}'
```
