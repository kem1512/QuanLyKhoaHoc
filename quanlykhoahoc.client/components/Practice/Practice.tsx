import useSWR from "swr";
import Loading from "../Loading/Loading";
import { IDoHomework, PracticeClient } from "../../app/web-api-client";
import {
  Alert,
  Badge,
  Button,
  Card,
  Center,
  Group,
  Stack,
} from "@mantine/core";
import { useState } from "react";
import DoHomework from "../DoHomework/DoHomework";

export default function Practice({
  subjectDetailId,
}: {
  subjectDetailId: number;
}) {
  const [doHomework, setDoHomework] = useState<IDoHomework>();

  const { data, isLoading } = useSWR(`/api/practice/${subjectDetailId}`, () =>
    new PracticeClient().getEntities(subjectDetailId, null, null, null, null)
  );

  return isLoading ? (
    <Loading />
  ) : doHomework?.practiceId ? (
    <DoHomework
      practice={data.items.find((c) => c.id == doHomework.practiceId)}
    />
  ) : (
    <Stack>
      {data?.items?.length > 0 ? (
        data.items.map((item) => {
          return (
            <Card shadow="md" p={"xl"}>
              <Card.Section>{item.title}</Card.Section>
              <Card.Section>
                <Group justify="space-between">
                  <Center>
                    <Badge me={"xs"}>{item.topic}</Badge>
                    <Badge color="grape">{item.level}</Badge>
                  </Center>
                  <Button
                    size="xs"
                    onClick={() =>
                      setDoHomework((prev) => ({
                        ...prev,
                        practiceId: item.id,
                      }))
                    }
                  >
                    Làm Bài
                  </Button>
                </Group>
              </Card.Section>
            </Card>
          );
        })
      ) : (
        <Alert>Không Có Gì Ở Đây Cả</Alert>
      )}
    </Stack>
  );
}
