import api from "@/components/Requisições/Urlapi"
import { auth } from "./Auth"
export const getMyPedidos = async () => {
    try {
        const response = await api.get("api/Pedido/meuspedidos", {
            headers: {
                Authorization: `Bearer ${sessionStorage.getItem('accessToken')}`
            }
        });
        return response.data;
    } catch (error) {
        if (error.response && error.response.status === 401) {
            if (await auth()) {
                console.warn("Tentando obter os pedidos novamente após renovar o token...");
                return await getMyPedidos();
            } else {
                console.warn("Falha ao renovar o token de acesso. Redirecionando para a página de login...");
                return false;
            }
        }
        console.error("Erro ao obter os pedidos:", error);
        throw error;
    }
}