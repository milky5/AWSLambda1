# memo

`sln`があるディレクトリで下記コマンドを実行

```sh
dotnet publish --framework netcoreapp3.1 --runtime linux-x64
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

## scratch に shell を入れる

<details><summary>失敗したやつ</summary>

```shell
otool -L /bin/zsh

```

```txt
	/usr/lib/libiconv.2.dylib (compatibility version 7.0.0, current version 7.0.0)
	/usr/lib/libSystem.B.dylib (compatibility version 1.0.0, current version 1281.100.1)
	/usr/lib/libncurses.5.4.dylib (compatibility version 5.4.0, current version 5.4.0)
```

```shell
mkdir work
cd work
cp /bin/zsh ./
cp /usr/lib/libiconv.2.dylib ./
cp /usr/lib/libSystem.B.dylib ./
cp /usr/lib/libncurses.5.4.dylib ./
```

```Dockerfile
FROM scratch
COPY ./work/zsh /bin/zsh
COPY ./work/libiconv.2.dylib /lib64/libiconv.2.dylib
COPY ./work/libSystem.B.dylib /lib64/libSystem.B.dylib
COPY ./work/libncurses.5.4.dylib /lib64/libncurses.5.4.dylib
CMD ["/bin/zsh"]
```

```sh
docker build -t scratchwithzsh .
```

```sh
docker run -it scratchwithzsh /bin/bash
docker run -p 9000:8080 scratchwithzsh
```

</details>

```shell
docker run -it centos /bin/bash
```

```shell
ldd /bin/bash
linux-vdso.so.1 (0x00007ffe2dffe000)
libtinfo.so.6 => /lib64/libtinfo.so.6 (0x00007f307d856000)
libdl.so.2 => /lib64/libdl.so.2 (0x00007f307d652000)
libc.so.6 => /lib64/libc.so.6 (0x00007f307d28f000)
/lib64/ld-linux-x86-64.so.2 (0x00007f307dda1000)
```

```shell
cd /lib64
ls -l
```

シンボリックリンク先を探してコピーする

```shell
docker cp 78ec18a611bc:/bin/bash ./work/bash/bash
docker cp 78ec18a611bc:/lib64/libtinfo.so.6.1 ./work/bash/libtinfo.so.6
docker cp 78ec18a611bc:/lib64/libdl-2.28.so ./work/bash/libdl.so.2
docker cp 78ec18a611bc:/lib64/libc-2.28.so ./work/bash/libc.so.6
docker cp 78ec18a611bc:/lib64/ld-2.28.so ./work/bash/ld-linux-x86-64.so.2
```

```Dockerfile
FROM scratch
COPY /work/bash/bash /bin/bash
COPY /work/bash/libtinfo.so.6 /lib64/libtinfo.so.6
COPY /work/bash/libdl.so.2 /lib64/libdl.so.2
COPY /work/bash/libc.so.6 /lib64/libc.so.6
COPY /work/bash/ld-linux-x86-64.so.2 /lib64/ld-linux-x86-64.so.2
CMD ["/bin/bash"]

```

```sh
docker build -t scratchwithzsh .
```

```sh
docker run -it scratchwithzsh /bin/bash
docker run -p 9000:8080 scratchwithzsh
```

```
./configure --prefix=/usr/local/curl/7_78_0
make
make install
```
