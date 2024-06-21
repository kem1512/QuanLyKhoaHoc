"use client";

import store from "../lib/store";
import { login, setAccessToken } from "../lib/userSlice";
import { AuthClient } from "../app/web-api-client";
import Cookies from "js-cookie";
import React, { useEffect, useCallback, useState, use } from "react";
import { Provider, useDispatch, useSelector } from "react-redux";
import { ToastContainer, toast } from "react-toastify";
import { MantineProvider } from "@mantine/core";
import { ModalsProvider } from "@mantine/modals";

// Create ClientService once
const AuthService = new AuthClient();

// New component to handle authentication
function AuthHandler({ onAuthComplete }: { onAuthComplete: () => void }) {
  const dispatch = useDispatch();
  const [isLoading, setIsLoading] = useState(true);

  const accessToken = useSelector((state: any) => state.auth.accessToken);
  const refreshToken = Cookies.get("refreshToken");
  const user = useSelector((state: any) => state.auth.user);

  const refreshAuthToken = useCallback(async () => {
    if (!accessToken && refreshToken) {
      try {
        const response = await AuthService.refreshAccessToken(refreshToken);

        const { accessToken: newAccessToken, refreshToken: newRefreshToken } =
          response;

        if (newAccessToken) {
          dispatch(setAccessToken(newAccessToken));
        }

        if (newRefreshToken) {
          Cookies.set("refreshToken", newRefreshToken, { expires: 30 });
        }

        if (!user) {
          const response = await fetch("/api/account", {
            headers: {
              Authorization: `Bearer ${newAccessToken}`,
            },
          });

          if (response.ok) {
            const data = await response.json();
            dispatch(login(data));
          }
        }
      } catch {
        toast.error("Lỗi Gì Đó");
      } finally {
        setIsLoading(false);
        onAuthComplete();
      }
    } else {
      setIsLoading(false);
      onAuthComplete();
    }
  }, [dispatch, refreshToken, accessToken, user, onAuthComplete]);

  useEffect(() => {
    refreshAuthToken();
  }, [refreshAuthToken]);

  if (isLoading) {
    return null;
  }

  return null;
}

export default function Template({ children }: { children: React.ReactNode }) {
  const [isAuthComplete, setIsAuthComplete] = useState(false);

  const handleAuthComplete = () => {
    setIsAuthComplete(true);
  };

  return (
    <Provider store={store}>
      <ToastContainer />
      <MantineProvider>
        <ModalsProvider>
          <AuthHandler onAuthComplete={handleAuthComplete} />
          {isAuthComplete && children}
        </ModalsProvider>
      </MantineProvider>
    </Provider>
  );
}
