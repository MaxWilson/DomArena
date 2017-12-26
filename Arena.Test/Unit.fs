module Arena.Test.Unit

open Xunit
open Arena.State
open Arena.Model

[<Fact>]
let ``Check cost function``() =
  let info =
    {
      nations =
        [|
        {
          name = "Beringia"
          id = 7
          units = [|{ UnitInfo.Unit 3 "Footpad" with gcost = 25; rcost = 20; recpoints = 14 }|]
          commanders = [|{ UnitInfo.Commander 4 "Wizard" with isCapOnly = true; gcost = 200 }|]
          }
        {
          name = "Saxony"
          id = 8
          units = [|{UnitInfo.Unit 5 "Knight" with gcost = 50; rcost = 60; recpoints = 20 }|]
          commanders = [|{ UnitInfo.Commander 6 "Witch" with isCapOnly = true; gcost = 180; rcost = 4 }|]
          }
        |]
      items = Array.empty
    }
  let nation1 = {
    id = Id 7
    research = Array.empty
    commanders =
      [|
        {
          CommanderSetup.Commander 4 with CommanderSetup.quantity = 3
          }
        {
          CommanderSetup.Commander 4
          with
            quantity = 3;
            units = [|Id(3), 30; Id(5), 4|]
          }
        |]
  }
  let nation2 = {
    id = Id 8
    research = Array.empty
    commanders =
      [|
        CommanderSetup.Commander 6
        {
          CommanderSetup.Commander 6 with units = [|Id(3), 30; Id(5), 4|]
          }
        |]
    }
  let gcost = (fun (x: UnitInfo) -> x.gcost)
  let rcost = (fun (x: UnitInfo) -> x.rcost)
  let reccost = (fun (x: UnitInfo) -> x.recpoints)
  let commcost = (fun (x: UnitInfo) -> x.commandPoints)
  Assert.Equal(1200+(3*(30*25+4*50)), (costOf info nation1 gcost))
  Assert.Equal(2*180+(30*25+4*50), (costOf info nation2 gcost))
  Assert.Equal(6+(3*(30*20+4*60)), (costOf info nation1 rcost))
  Assert.Equal(2*4+(30*20+4*60), (costOf info nation2 rcost))
  Assert.Equal(3*(30*14+4*20), (costOf info nation1 reccost))
  Assert.Equal(30*14+4*20, (costOf info nation2 reccost))
  Assert.Equal(12, (costOf info nation1 commcost))
  Assert.Equal(4, (costOf info nation2 commcost))
