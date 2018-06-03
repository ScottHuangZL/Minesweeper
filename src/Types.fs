module Minesweeper.Types
// open System.ComponentModel

type BoxIndex = int

type Msg =
  | Reveal of BoxIndex
  | ToggleFlagMine of BoxIndex
  | ResetGame
  | SetMinesCount of int
  | SetSize of int

type Model =
  {
    gameState: GameState
    map: BoxState[]
    revealed: Map<BoxIndex, RevealedState>
    minesCount : int
    size : int
  }
and GameState =
  | Running
  | Won
  | Lost of detonatedMine: BoxIndex
and RevealedState = 
  | Open
  | FlaggedMine
and BoxState =
  | Mine
  | Empty
  | MineProximity of neighbourMines: int
