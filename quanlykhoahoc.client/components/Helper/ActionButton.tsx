import { Button } from "@mantine/core";
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
