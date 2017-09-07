# Chomado Problem Server

## これは何?

"ちょまど問題" を解くアプリを作る際に、回答を問い合わせるのに使える Web API サーバーです。

このサイトに固定で設定されている、4択問題x全10問の回答を、JSON 形式で POST すると、正答数を返します。

## "ちょまど問題" とは?

> ![@ito_yusaku](https://pbs.twimg.com/profile_images/477275642065473537/N7VoaKoW_normal.jpeg) _伊藤 祐策(パソコンの大先生) (@ito_yusaku)_  
> **【ちょまど問題まとめ】**  
>・4択問題が全部で10問ある。  
>・全部の回答を終えると何問正解したかだけが分かる。  
>・全問正解するとクリア。  
>・問題文は人外の言葉で書かれているので読んでも解けない。  
>・回答試行は何回でもできるが、できるだけ少ない回数でクリアしたい。  
> &mdash; <a href="https://twitter.com/ito_yusaku/status/479262891124617216">June 18, 2014</a>

## リクエスト送信方法

**ホスト:**  
以下の3種類の PaaS 上に常設しています。

- **AppHarbor:** https://chomado-problem-server.apphb.com/
- **Heroku:** https://chomado-problem-server.herokuapp.com/
- **Microsoft Azure Web Apps:** https://chomado-problem-server.azurewebsites.net/

**API エンドポイントのパス**

`/answer`

**メソッド:**  `POST`

**必要な要求ヘッダ:** `content-type: application/json`

**送信ボディ:**  
4択の回答を 1, 2, 3, 4 の整数のいずれかで表し、10問分の回答となる要素10個の配列を JSON 形式で送信。

例) `[1,2,3,4,1,2,3,4,1,2]`

## 呼び出し例

例えば、cURL を使って下記のように回答 "[1,2,3,4,1,2,3,4,1,2]" を POST すると、正答数が返ります。

```
$ curl https://chomado-problem-server.apphb.com/answer -X POST -d "[1,2,3,4,1,2,3,4,1,2]" -H "content-type:application/json"
2
```

## 自分で設置する

### Microsoft Azure Web Apps に設置する

下の「Deploy to Azure」ボタンをクリックし、表示される Web サイトの指示に従ってください。

[![Deploy to Azure](https://azuredeploy.net/deploybutton.png)](https://azuredeploy.net/)

Chomado Problem Server は Microsoft Azure Web Apps の無料枠内で実行できます。

### Heroku に設置する

Chomado Problem Server は、Docker Hub にて Docker イメージとしても配布しています (下記)。

https://hub.docker.com/r/jsakamoto/chomad-problem-server/

この Docker イメージを Heroku の Docker コンテナに配置することで Heroku 上への Chomado Problem Server の設置が可能です。

Heroku CLI と Docker がインストール済みの環境であれば、下記の手順で配置可能です (下記の `{appname}` の部分は、設置しようとしている Heroku 上の実際のアプリ名に置き換えてください)。

```bash
# "heroku update" で heroku CLI を最新版に更新しておくこと
# 事前に "heroku login" 及び "heroku container:login" で
# Heroku とそのコンテナサービスへのログインを済ませておくこと
$ heroku apps:create {appname} # すでにアプリを別途作成済みなら不要

# Chomado Problem Server の Docker イメージをローカル環境に持ってくる
$ docker pull jsakamoto/chomad-problem-server:latest

# ローカルに持ってきた Chomado Problem Server の Docker イメージに、
# Heroku の Docker リポジトリ名で別名をつける
$ docker tag jsakamoto/chomad-problem-server:latest registry.heroku.com/{appname}/web:latest

# その別名で docker push することで、Chomado Problem Server の
# Docker イメージが Heroku の Docker リポジトリに送り込まれ、
# Heroku 上で自動で docker run されて稼働が始まる。
$ docker push registry.heroku.com/{appname}/web:latest

# docker push が成功したら、"heroku open -a {appname}" で、
# デフォルトブラウザにて Chomado Problem Server のページが開く
```

なお、こうして Heroku の Docker コンテナサービスに配置したChomaod Problem Server の Web ページについて、HTTPS アクセスを強制するには、`EnforceHTTP` 環境変数に `true` を設定してください。

Heroku CLI であれば、下記コマンドになります。

```bash
$ heroku config:set EnforceHTTPS=true
```

以上の設定を施しておくと、Chomado Problem Server の説明ページへの HTTP プロトコルでのアクセスは HTTPS プロトコルでのアクセスにリダイレクトされるようになります (Web API エンドポイントについては、下位互換維持のため、HTTP から HTTPS へのリダイレクトは行いません)。

Chomado Problem Server は Heroku の無料枠内で実行できます。

## 開発

Chomado Problem Server は C# + .NET Core 1.1 + ASP.NET Core 1.1 で作成されています。

開発環境は Windows OS + Visual Studio 2017 以降 (Community Edition 可) を想定しています。

このリポジトリを git clone したのち、ソリューションファイル(.sln)を Visual Studio で開いてキーボードの F5 を押せばビルドが実行され、続けてブラウザが起動してページが表示されます。

## ライセンス

[GNU General Public License v2.0](https://github.com/jsakamoto/chomado-problem-server/blob/master/LICENSE)