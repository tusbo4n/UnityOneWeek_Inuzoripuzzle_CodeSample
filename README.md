# UnityOneWeek_Inuzoripuzzle_CodeSample

Unity1Week参加作品「犬ぞりパズル！」の、自作コードを抜粋したポートフォリオ用リポジトリです。

## 作品概要

「犬ぞりパズル！」は、Unity1Weekのお題に沿って制作・公開したUnity製ゲームです。

犬を操作して、犬に繋がれたプレゼントを家へ届けるパズルゲームです。
犬やプレゼントが海に落ちた場合はミスとなります。

短期間の制作において、ゲームルールの設計、Unity / C#による実装、UIや演出の調整、公開までを一貫して行いました。

作品URL：https://unityroom.com/games/inuzoripuzzle

## 掲載内容

本作品で使用した、ゲーム進行やキャラクター制御、UI、ギミックに関するコードを掲載しています。

- `DirSelectUI.cs`  
  プレイヤーが進行方向を選択するためのUI制御

- `GameSceneCanvas.cs`  
  ゲーム画面上のUI表示や更新処理

- `GameSceneController.cs`  
  ゲーム全体の進行管理

- `House.cs`  
  プレゼントの届け先となる家の処理

- `Inu.cs`  
  犬の移動や状態を管理する処理

- `InuBouncer.cs`  
  タイトル画面で、画面外へ移動した犬を反転させて戻す演出

- `Present.cs`  
  プレゼントの落下判定、ひきずる効果音、状態を管理する処理

## ファイル構成

```text
UnityOneWeek_Inuzoripuzzle_CodeSample
├─ DirSelectUI.cs
├─ GameSceneCanvas.cs
├─ GameSceneController.cs
├─ House.cs
├─ Inu.cs
├─ InuBouncer.cs
├─ Present.cs
└─ README.md

```

## 補足

本リポジトリは、採用選考においてコードの書き方や実装方針を確認していただくことを目的として、本作品で使用した自作コードの一部を抜粋して公開しています。

素材、音楽、画像、外部アセット、`.meta`ファイル等は含めていないため、このリポジトリ単体ではUnityプロジェクトとして動作しません。

実際のゲームは、上記のunityroomリンクから確認できます。
