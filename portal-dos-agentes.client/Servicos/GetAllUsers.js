import api from "@/components/Requisições/Urlapi";
import { auth } from "./Auth";

export async function getAllUsers() {
    try {
        const response = await api.get("api/User", {
            headers: {
                Authorization: `Bearer ${sessionStorage.getItem('accessToken')}`
            }
        });
        if (response.status === 200) {
            console.log("Lista de usuários obtida com sucesso.");
        }
        return response.data; // Retorna a lista de usuários
    } catch (error) {
        if (error.response && error.response.status === 401) {
           if(await auth()){
               console.warn("Tentando obter a lista de usuários novamente após renovar o token...");
               return await getAllUsers(); // Tenta novamente após renovar o token
           } else {
               console.warn("Falha ao renovar o token de acesso. Redirecionando para a página de login...");
               return false
           }
        }
        console.error("Erro ao obter a lista de usuários:", error);
        throw error; // Lança o erro para ser tratado em outro lugar
    }
}