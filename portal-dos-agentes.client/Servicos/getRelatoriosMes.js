import api from "@/components/Requisições/Urlapi";
import { auth } from "./Auth";

export const GetRelatorioMes = async (ano, mes)=>{
    try {
        const response = await api.get(
          `/api/Mes/GetDaysMes?ano=${ano}&mes=${mes}`,
          {
            headers: {
              Authorization: `Bearer ${sessionStorage.getItem('accessToken')}`
            }
          }
        )
        return response.data
    }catch (error) {
        console.error(error)
        return null // Opcional: evita erros em quem chamar
    }
}