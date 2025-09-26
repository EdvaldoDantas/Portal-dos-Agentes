import api from "@/components/Requisições/Urlapi";
import { auth } from "./Auth";

export const post = async (preco) => {
    try {
        const response = await api.post(`api/Pedido/${preco}`, null, {
            headers: {
                Authorization: `Bearer ${sessionStorage.getItem('accessToken')}`
            }
        });
        return response.data;
    } catch (error) {
        if (error.response && error.response.status === 401) {
            if (await auth()) {
                console.warn("Tentando realizar o pedido novamente após renovar o token...");
                return await post(preco);
            } else {
                console.warn("Falha ao renovar o token de acesso. Redirecionando para a página de login...");
                return false;
            }
        }
        console.error("Erro ao realizar o pedido:", error);
        throw error;
    }
};
