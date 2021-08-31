# memo

## docker image 作成まで

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

## docker image を ecr に アップロード

`aws configure`で設定を確認

```sh
aws s3api list-buckets
```

とかで、正しいアカウントでアクセスできていることがわかる

[こちらの 7,8,9](https://docs.aws.amazon.com/ja_jp/lambda/latest/dg/images-create.html#images-create-from-base)

```sh
# 123456789012 を自分のアカウントに変える
aws ecr get-login-password --region ap-northeast-1 | docker login --username AWS --password-stdin 123456789012.dkr.ecr.ap-northeast-1.amazonaws.com
```

```sh
aws ecr create-repository --repository-name mylambda --image-scanning-configuration scanOnPush=true --image-tag-mutability MUTABLE
```

```sh
# 123456789012 を自分のアカウントに変える
docker tag mylambda:latest 123456789012.dkr.ecr.ap-northeast-1.amazonaws.com/mylambda:latest

docker push 123456789012.dkr.ecr.ap-northeast-1.amazonaws.com/mylambda:latest
```
