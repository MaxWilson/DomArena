module Arena.Model

type Path = Fire | Water | Earth | Air | Astral | Death | Nature | Blood
type School = Conj | Alt | Evoc | Const | Ench | Thaum | Blood

type UnitInfo = {
  name: string
  id: int
  isCommander: bool
  isCapOnly: bool
  gcost: int
  rcost: int
  recpoints: int
  commandPoints: int
  }
  with
  static member Unit id name = { name = name; id = id; isCommander = false; isCapOnly = false; gcost = 1; rcost = 1; recpoints = 1; commandPoints = 0 }
  static member Commander id name = { name = name; id = id; isCommander = true; isCapOnly = false; gcost = 1; rcost = 1; recpoints = 0; commandPoints = 2 }

type NationInfo = {
  name: string
  id: int
  commanders: UnitInfo []
  units: UnitInfo []
  }

type ItemInfo = {
  name: string
  cost: (Path * int) []
  }

type Info = {
  nations: NationInfo []
  items: ItemInfo []
  }

type Id = Id of int | Name of string

type CommanderSetup = {
  id: Id
  quantity: int
  item: Id []
  units: (Id * int) []
  }
  with
  static member Commander id = { id = Id id; quantity = 1; item = Array.empty; units = Array.empty }

type NationSetup = {
  id: Id
  commanders: CommanderSetup []
  research: (School * int) []
  }

type GameSetup = {
  nation1: NationSetup
  nation2: NationSetup
  }