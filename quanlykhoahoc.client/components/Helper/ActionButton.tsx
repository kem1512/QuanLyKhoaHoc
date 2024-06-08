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

  const handleAction = () => {
    setIsLoading(true);
    action().then(() => {
      setIsLoading(false);
    });
  };

  return (
    <Button
      {...props}
      loading={isLoading}
      onClick={() => !isLoading && handleAction()}
    ></Button>
  );
}
