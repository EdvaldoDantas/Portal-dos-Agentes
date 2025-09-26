import api  from "@/components/Requisições/Urlapi"
import { auth } from "./Auth"
export const get = async () =>{
    try {
        const response = await api.get('api/Pedido', {
            headers: {
                Authorization: `Bearer ${sessionStorage.getItem('accessToken')}`
            }
        })
        return response.data
    } catch (error) {
        if (error.response && error.response.status === 401) {
           if(await auth()){
               console.warn("Tentando obter a lista de pedidos novamente após renovar o token...");
               return await get(); // Tenta novamente após renovar o token
           } else {
               console.warn("Falha ao renovar o token de acesso. Redirecionando para a página de login...");
               return false
           }
        }
        console.error("Erro ao obter a lista de usuários:", error);
        throw error; // Lança o erro para ser tratado em outro lugar
    }
}