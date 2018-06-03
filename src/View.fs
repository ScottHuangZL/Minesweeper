module Minesweeper.View

open Fable.Helpers.React
open Fable.Helpers.React.Props
open Fable.Core.JsInterop
open Types
open GameLogic

let title content = h1 [ClassName "title"] [str content]
let subtitle content = h2 [ClassName "subtitle"] [str content]
let showIcon label = span [ClassName "icon"] [i[ClassName <| sprintf "fa fa-%s" label][]]

let mineBoxFontSize model =
    let fontSize = (240 * 8 / model.size) |> int
    if fontSize < 30 
        then sprintf "%i%%" 30
        else if fontSize > 240
            then sprintf "%i%%" 240
            else sprintf "%i%%" fontSize

let showGrid titleText presentBox model dispatch =
    let cellsTemplate = sprintf "repeat(%i, 1fr)" <| getSize model.map
    div 
        [ ]
        [
            title titleText

            model.map
            |> Array.mapi presentBox
            |> Array.toList
            |> div 
                [
                    ClassName "minefield"
                    Style [
                        GridTemplateColumns cellsTemplate
                        GridTemplateRows cellsTemplate
                        // FontSize (sprintf "%i%%" model.boxFontSize)
                        FontSize (mineBoxFontSize model)
                    ]
                ]

            br []    
            div [][
                table[][
                  tbody[][
                    tr []
                        [
                        td [ClassName "is-one-fifth"]
                            [
                            label [] [str "Mines Count (Min is 1, Max cannot > size * size):" ]    
                            input
                                [ ClassName "input"
                                //   Type "text"
                                  Placeholder "Mines Count"
                                  DefaultValue (model.minesCount |> string)
                                //   AutoFocus true
                                  OnChange (fun ev -> 
                                    !!ev.target?value 
                                        |> validateMinesCount model.size|> SetMinesCount |> dispatch 
                                    // ev.target?value <- validateMinesCount model.size model.minesCount
                                  ) ]
                            ]
                        td [ClassName "is-one-fifth"]
                            [
                            label [] [str "Grid Size (Min is 4, Max is 30):"]    
                            input
                                [ ClassName "input"
                                //   Type "text"
                                  Placeholder "Grid Size"
                                  DefaultValue (model.size |> string)
                                //   AutoFocus true
                                  OnChange (fun ev -> 
                                    !!ev.target?value 
                                        |> validateSize model.minesCount 
                                        |> SetSize |> dispatch 
                                    // ev.target?value <- validateSize model.minesCount model.size    
                                  ) ]
                            ]
                        td [ClassName "is-one-fifth"]
                            [
                            label [] [str "Calculated Font Size:"]
                            label [] [str (mineBoxFontSize model)]    
                            br [] 
                            label [] [str (sprintf "Calculated MinesCount = %i" (validateMinesCount model.size model.minesCount)) ]                     
                            br [] 
                            label [] [str (sprintf "Calculated Size = %i" (validateSize model.minesCount model.size)) ]   
                            ]
                        ]
                    ]
                ]
            ]    
            button [OnClick (fun _ -> dispatch ResetGame)] [str "Reset the game"]
        ]

let showBox isDetonatedMine _ box  =
    match box with
    | Mine -> 
        let className = if isDetonatedMine then "minebox detonated" else "minebox"
        span [ ClassName className] [ showIcon "bomb"]
    | MineProximity nearbyMines-> span [ ClassName (sprintf "minebox color-number-%i" nearbyMines)] [str <| string nearbyMines] 
    | Empty -> span [ ClassName "minebox"] []

let private showRawHotBox dispatch boxIndex content = 
        button 
            [
                ClassName "minebox hot-minebox"
                OnClick (fun _ -> dispatch (Reveal boxIndex))
                OnContextMenu (fun e -> 
                    e.preventDefault()
                    dispatch (ToggleFlagMine boxIndex)
                )
            ]
            content 

let showHotBox dispatch mbRevealedState boxIndex box =
    match mbRevealedState with
    | Some Open -> showBox false boxIndex box
    | Some FlaggedMine ->
        [showIcon "question"] |> showRawHotBox dispatch boxIndex 
    | None -> showRawHotBox dispatch boxIndex []  

    


let view model dispatch = 
    let partialApp =
        match model.gameState with
        | Won -> 
            showGrid 
                "Congradulations you won!!!" 
                (showBox false)
        | Lost detonatedMine -> 
            showGrid 
                "Oups you triggered a mine!" 
                (fun boxIndex box -> showBox (boxIndex = detonatedMine) boxIndex box)
        | Running -> 
            showGrid 
                "Go on with care"
                (fun boxIndex box -> showHotBox dispatch (model.revealed |> Map.tryFind boxIndex) boxIndex box)

    partialApp model dispatch