import { auth } from "./Auth"
import api from "@/components/Requisições/Urlapi"

export const getRelatorio = async (agenteId, mes, ano) => {
  try {
    const response = await api.get(
      `api/Mes/GetByDate?mes=${mes}&ano=${ano}&userId=${agenteId}`,
      {
        headers: {
          Authorization: `Bearer ${sessionStorage.getItem('accessToken')}`
        }
      }
    )
    return response.data
  } catch (error) {
    console.error(error)
    return null // Opcional: evita erros em quem chamar
  }
}
