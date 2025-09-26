import api from "@/components/Requisições/Urlapi";
import { auth } from "./Auth";

export const putPedido = async (pedidoId) => {
    try {
        const response = await api.put(`api/Pedido?pedidoId=${pedidoId}`, null,{
            headers: {
                Authorization: `Bearer ${sessionStorage.getItem('accessToken')}`
            }
        });
        return response.data;
    } catch (error) {
        if (error.response && error.response.status === 401) {
            if (await auth()) {
                console.warn("Tentando atualizar o pedido novamente após renovar o token...");
                return await putPedido(pedidoId, isAceite);
            } else {
                console.warn("Falha ao renovar o token de acesso. Redirecionando para a página de login...");
                return false;
            }
        }
        console.error("Erro ao atualizar o pedido:", error);
        throw error;
    }
}