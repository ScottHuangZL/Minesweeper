module Minesweeper.State

open Elmish
open Types
open GameLogic
open Fable

let init minesCount size =
  {
    minesCount = minesCount
    size = size
    Model.map = generateMineField minesCount size
    revealed = Map.empty<BoxIndex, RevealedState>
    gameState = Running
  }, Cmd.none


let firstInit _ =
  init 10 8

let update msg model = 
  let newModel =
    match msg with 
    | ResetGame -> fst <| init model.minesCount model.size
    | SetMinesCount minesCount -> {model with minesCount = minesCount}
    | SetSize size -> 
        {model with size = size}
    | Reveal boxIndex ->
        match model.map.[boxIndex], model.revealed |> Map.tryFind boxIndex with
        | Mine, _ -> {model with gameState = Lost boxIndex}
        | _, Some Open -> model
        | MineProximity _ , _ ->{ model with revealed = model.revealed |> Map.add boxIndex Open}
        | Empty, _ -> {model with revealed = revealEmptyFieldsAndNeighbours model boxIndex}
    | ToggleFlagMine boxIndex->
        match model.revealed |> Map.tryFind boxIndex with
        | Some Open -> model
        | Some FlaggedMine -> {model with revealed = model.revealed |> Map.remove boxIndex}
        | None -> {model with revealed = model.revealed |> Map.add boxIndex FlaggedMine}

  match model.gameState with
  | Won -> newModel, Cmd.none
  | Lost _ -> newModel, Cmd.none
  | Running ->
      let toOpen = newModel.map |> Array.sumBy(function Mine -> 0 | _ -> 1)
      let opened = newModel.revealed |>Seq.sumBy(fun kvp -> match kvp.Value with Open -> 1 | _ -> 0)
      if toOpen  = opened
        then {newModel with gameState = Won}, Cmd.none
        else newModel, Cmd.none