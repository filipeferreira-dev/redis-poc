# Dependêcias

- Sql Server
- Redis

# Preparando denpendências

Antes de tudo, você vai precisar ter o docker rodando na sua máquina.
Caso ainda não tenha siga [esse tutorial](https://docs.docker.com/docker-for-windows/install/).

### Subindo Redis

Execute o comando a seguir no terminal:
```
docker run -p 6379:6379 --name some-redis -d redis
```

### Subindo Sql Server

Execute o comando a seguir no terminal:

* Subistitua a chave `<YourStrong@Passw0rd>` pela senha que você quer utilizar no sql.
* Altere a connection string no arquivo [MerchantRepository.cs](https://github.com/filipeferreira-dev/redis-poc/blob/master/poc-list-merchant-cache/Repositories/MerchantRepository.cs)

```
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=<YourStrong@Passw0rd>" -p 1433:1433 --name some-sql -d mcr.microsoft.com/mssql/server:2017-latest
```
