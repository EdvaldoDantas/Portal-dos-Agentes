import api from "@/components/Requisições/Urlapi";
import { auth } from "./Auth";
export const deleteAgente = async (id) => {
    try {
        const response = await api.delete('/api/User/' + id, {
            headers: {
                Authorization: 'Bearer ' + sessionStorage.getItem('accessToken')
            }
        });
        if (response.status === 200) {
            alert('Agente eliminado com sucesso.');
        }
    } catch (error) {
        if (error.response && error.response.status === 401) {
            if (await auth()) {
                console.warn("Tentando eliminar o agente novamente após renovar o token...");
                return await deleteAgente(id); // Tenta novamente após renovar o token
            } else {
                console.warn("Falha ao renovar o token de acesso. Redirecionando para a página de login...");
                return false; // Redireciona para a página de login
            }
        }
        console.error('Erro ao eliminar agente:', error);
    }
};