import { HubConnection, HubConnectionBuilder } from "@microsoft/signalr";
import { ref } from "vue";

const baseUrl = "https://localhost:7032";

export default function useHub() {
  const connection = ref<HubConnection>(
    new HubConnectionBuilder().withUrl(`${baseUrl}/room`).build()
  );

  async function getConnection() {
    if (!connection.value) {
      connection.value = new HubConnectionBuilder().withUrl(`${baseUrl}/room`).build();
    }

    await connection.value.start();

    //await connection.start();
    return connection.value;
  }

  return {
    //connection,
    getConnection,
  };
}
