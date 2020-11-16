// {rank: 7, suit: 4, type: 1, description: "Семерка крести"}
import { images } from "../../assets/index"
const suits = [0, "D", "H", "S", "C"]
const extraRank = [0, "J", "Q", "K", "A"]

export default function mapCardToImgSrc({ rank, suit }) {
  if (suit === 0) {
    return null
  }
  let cardSrc = "/"
  if (rank <= 10) {
    cardSrc += rank
  } else {
    cardSrc += extraRank[rank - 10]
  }
  return images.find((image) => image.includes(cardSrc + suits[suit] + "."))
}
