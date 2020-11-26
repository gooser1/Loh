export class ApiService {
    sendCard = async (card) => {
        const requestOptions = {
          method: "POST",
            headers: {"Content-Type": "application/json" },
          body: JSON.stringify(card)
        }
        const res = await fetch("Room/cardToTable", requestOptions)
        if (!res.ok) {
          console.log(`Couldn't fetch ${res.url}, received ${res.status}`)
        }
        const body = await res.json()
        const response = await body
        return response
    }

  getHand = async () => {
    const response = await fetch("Room/myHand")
    const data = await response.json()
    return data
  }
}

/*
 один контроллер - Room

 Get:
 deck           оставшаяся колода, а точнее количество карт в ней и козырь
 gameStatus     статус игры (кто ходит и нне окончена ли игра)
 myHand         карты в руке
 myStatus       статус игрока 1 - ходит, 2 - отбивается, 3 - сидит, может подкидывать, 0 - не в игре
 
 Post:
 cardToTable    кинуть карту с руки на стол. Атака это или защита, можно или нельзя - решит сервер, пока нельзя выбирать, что именно кроешь
 imFinish       "беру", если кроюсь, и "не буду подкидывать" в остальных случаях
 */
