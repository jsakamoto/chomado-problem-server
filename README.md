# Chomado Problem Server

## これは何?

"ちょまど問題" を解くアプリを作る際に、回答を問い合わせるのに使える Web API サーバーです。

このサイトに固定で設定されている、4択問題x全10問の回答を、JSON 形式で POST すると、正答数を返します。

## "ちょまど問題" とは?

<blockquote class="twitter-tweet" lang="en"><p lang="ja" dir="ltr">【ちょまど問題まとめ】&#10;・4択問題が全部で10問ある。&#10;・全部の回答を終えると何問正解したかだけが分かる。&#10;・全問正解するとクリア。&#10;・問題文は人外の言葉で書かれているので読んでも解けない。&#10;・回答試行は何回でもできるが、できるだけ少ない回数でクリアしたい。</p>&mdash; 伊藤 祐策(パソコンの大先生) (@ito_yusaku) <a href="https://twitter.com/ito_yusaku/status/479262891124617216">June 18, 2014</a></blockquote>
<script async src="//platform.twitter.com/widgets.js" charset="utf-8"></script>

## リクエスト送信方法

**URL:**  
以下の3種類の PaaS 上に常設しています。

- **AppHarbor:** http://chomado-problem-server.apphb.com/answer
- **Heroku:** http://chomado-problem-server.heroku.com/answer
- **Microsoft Azure Web Apps:** http://chomado-problem-server.azurewebsites.net/answer

**メソッド:**  `POST`

**必要な要求ヘッダ:** `content-type: application/json`

**送信ボディ:**  
4択の回答を 1, 2, 3, 4 の整数のいずれかで表し、10問分の回答となる要素10個の配列を JSON 形式で送信。

例) `[1,2,3,4,1,2,3,4,1,2]`

## 呼び出し例

例えば、cURL を使って下記のように回答 "[1,2,3,4,1,2,3,4,1,2]" を POST すると、正答数が返ります。

```
$ curl http://chomado-problem-server.apphb.com/answer -X POST -d "[1,2,3,4,1,2,3,4,1,2]" -H "content-type:application/json"
2
```

## 自分で設置する

### Microsoft Azure Web Apps に設置する

下の「Deploy to Azure」ボタンをクリックし、表示される Web サイトの指示に従ってください。

[![Deploy to Azure](https://azuredeploy.net/deploybutton.png)](https://azuredeploy.net/)

Chomado Problem Server は Microsoft Azure Web Apps の無料枠内で実行できます。

### Heroku に設置する

下の「Deploy to Heroku」ボタンをクリックし、表示される Web サイトの指示に従ってください。

[![Deploy](https://www.herokucdn.com/deploy/button.png)](https://heroku.com/deploy)

Chomado Problem Server は Heroku の無料枠内で実行できます。

## ライセンス

[GNU General Public License v2.0](https://github.com/jsakamoto/chomado-problem-server/blob/master/LICENSE)