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
 ���� ���������� - Room

 Get:
 deck           ���������� ������, � ������ ���������� ���� � ��� � ������
 gameStatus     ������ ���� (��� ����� � ��� �������� �� ����)
 myHand         ����� � ����
 myStatus       ������ ������ 1 - �����, 2 - ����������, 3 - �����, ����� �����������, 0 - �� � ����
 
 Post:
 cardToTable    ������ ����� � ���� �� ����. ����� ��� ��� ������, ����� ��� ������ - ����� ������, ���� ������ ��������, ��� ������ ������
 imFinish       "����", ���� ������, � "�� ���� �����������" � ��������� �������
 */
