module Arena.State

open Model

let costOf (info: Info) (nation: NationSetup) costFunction =
  let allUnits = (info.nations |> Seq.collect (fun info -> Seq.append info.units info.commanders))
  let lookup = function
    | Id(id) ->
      allUnits |> Seq.find (fun u -> u.id = id)
    | Name(name) ->
      allUnits |> Seq.find (fun u -> u.name = name)
  nation.commanders |> Seq.sumBy (fun commander -> commander.quantity * (costFunction (lookup commander.id) + (commander.units |> Seq.sumBy (fun (id, qty) -> qty * (lookup id |> costFunction)))))