import { ActionIcon, Button } from "@mantine/core";
import { useState } from "react";

export default function ActionButton({
  action,
  ...props
}: {
  action: any;
  [key: string]: any;
}) {
  const [isLoading, setIsLoading] = useState<boolean>(false);

  const handleAction = async () => {
    setIsLoading(true);
    try {
      await action();
    } catch (error) {
    } finally {
      setIsLoading(false);
    }
  };

  return (
    <Button
      {...props}
      loading={isLoading}
      onClick={() => !isLoading && handleAction()}
    ></Button>
  );
}

export function ActionButtonIcon({
  action,
  children,
  ...props
}: {
  action: any;
  children: React.ReactNode;
  [key: string]: any;
}) {
  const [isLoading, setIsLoading] = useState<boolean>(false);

  const handleAction = async () => {
    setIsLoading(true);
    try {
      await action();
    } catch (error) {
    } finally {
      setIsLoading(false);
    }
  };

  return (
    <ActionIcon
      {...props}
      loading={isLoading}
      onClick={() => !isLoading && handleAction()}
    >
      {children}
    </ActionIcon>
  );
}
