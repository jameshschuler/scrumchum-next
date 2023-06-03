export default function useStorage() {
  function storeUser(userId: number) {
    localStorage.setItem("userId", JSON.stringify({ userId }));
  }

  return {
    storeUser,
  };
}
