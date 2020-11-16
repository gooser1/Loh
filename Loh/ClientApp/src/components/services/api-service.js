export class ApiService {
  sendCard = async (card) => {
    const requestOptions = {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(card),
    }
    const res = await fetch("card", requestOptions)
    if (!res.ok) {
      console.log(`Couldn't fetch ${res.url}, received ${res.status}`)
    }
    const body = await res.json()
    const response = await body
    return response
  }

  getAllCards = async () => {
    const response = await fetch("Cards")
    const data = await response.json()
    return data
  }
}
