import { createContext, useContext, useEffect, useState } from "react";
import { UserContext } from "./UserContextProvider";

export const WalletContext = createContext({
  wallet: null,
  refresh() {},
  isLoading: true,
});
const defaultWallet = { money: 0 };

function WalletContextProvider(props) {
  const [wallet, setWallet] = useState(defaultWallet);
  const [isLoading, setIsLoading] = useState(true);
  const userCTX = useContext(UserContext);

  useEffect(() => {
    if (userCTX.user) {
      fetchWallet();
    }
  }, [userCTX.user]);

  async function fetchWallet() {
    const currentPlayerWallet = await fetch("/api/player/wallet");
    if (currentPlayerWallet.ok) {
      setWallet(await currentPlayerWallet.json());
    } else {
      setWallet(defaultWallet);
    }
    setIsLoading(false);
  }

  const value = {
    wallet,
    isLoading,
    refresh() {
      fetchWallet();
    },
  };

  return (
    <WalletContext.Provider value={value}>
      {props.children}
    </WalletContext.Provider>
  );
}

export default WalletContextProvider;
