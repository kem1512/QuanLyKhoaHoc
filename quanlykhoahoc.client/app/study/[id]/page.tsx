"use client";

import { useSearchParams } from "next/navigation";
import StudyLayout from "../../../components/Layout/StudyLayout/StudyLayout";
import useSWR from "swr";
import {
  DoHomeworkClient,
  DoHomeworkCreate,
  DoHomeworkUpdate,
  IDoHomeworkMapping,
  PracticeClient,
} from "../../web-api-client";
import Loading from "../../../components/Loading/Loading";
import {
  Alert,
  Badge,
  Button,
  Card,
  Center,
  Group,
  Paper,
} from "@mantine/core";
import Link from "next/link";
import { Editor } from "@monaco-editor/react";
import { useEffect, useState } from "react";
import { handleSubmit } from "../../../lib/helper";
import { IconStarFilled } from "@tabler/icons-react";

export default function Study({ params }: { params: { id: number } }) {
  const { id } = params;
  const searchParams = useSearchParams();
  const subjectDetailId = searchParams.get("subjectDetailId");
  const practiceId = searchParams.get("practiceId");
  const DoHomeWorkService = new DoHomeworkClient();
  const [doHomeWork, setDoHomeWork] = useState<
    IDoHomeworkMapping | undefined
  >();

  const { data: doHomeWorkData, mutate } = useSWR(
    `/api/doHomework/${practiceId}`,
    () =>
      practiceId ? new DoHomeworkClient().getEntity(Number(practiceId)) : null
  );

  useEffect(() => {
    if (doHomeWorkData) setDoHomeWork(doHomeWorkData);
  }, [doHomeWorkData]);

  const { data, isLoading } = useSWR(
    `/api/practice/${subjectDetailId}`,
    () =>
      subjectDetailId
        ? new PracticeClient().getEntities(
            Number(subjectDetailId),
            true,
            null,
            null,
            null,
            null
          )
        : null,
    {
      revalidateOnFocus: false,
      revalidateOnReconnect: false,
      revalidateIfStale: false,
    }
  );

  return (
    <StudyLayout params={params} subjectDetailId={subjectDetailId}>
      <Paper my={"xs"}>
        {isLoading ? (
          <Loading />
        ) : data?.items?.length > 0 ? (
          practiceId ? (
            <>
              {data.items.find((c) => c.id.toString() === practiceId)?.testCases
                .length ? (
                <Editor
                  height="75vh"
                  defaultLanguage="javascript"
                  defaultValue={data.items
                    .find((c) => c.id.toString() === practiceId)
                    ?.testCases.map(
                      (c, i) =>
                        `// Input: ${c.input} // Output: ${c.output} ${
                          doHomeWorkData &&
                          `, Kết Quả Của Bạn: ${doHomeWorkData.testCases[i].result} - Thời Gian Chạy: ${doHomeWorkData.testCases[i].runTime} ms`
                        }`
                    )
                    .join("\n")}
                  options={{
                    minimap: { enabled: false },
                    overviewRulerBorder: false,
                    wordWrap: "on",
                  }}
                  className="overflow-hidden"
                  onChange={(e) =>
                    setDoHomeWork((prev) => ({
                      ...prev,
                      actualOutput: e,
                      practiceId: Number(practiceId),
                      registerStudyId: 10,
                    }))
                  }
                />
              ) : (
                <Alert>Không có test case</Alert>
              )}
              <Button
                onClick={() =>
                  handleSubmit(
                    () =>
                      doHomeWork.id
                        ? DoHomeWorkService.updateEntity(
                            doHomeWork.id,
                            doHomeWork as DoHomeworkUpdate
                          )
                        : DoHomeWorkService.createEntity(
                            doHomeWork as DoHomeworkCreate
                          ),
                    "Nộp Bài Thành Công",
                    mutate
                  )
                }
              >
                Nộp Bài
              </Button>
            </>
          ) : (
            data.items.map((item) => (
              <Card key={item.id} shadow="md" p="xl">
                <Card.Section mb={"xs"}>
                  <Group justify="space-between">
                    {item.title}
                    {item.isRequired ? (
                      <IconStarFilled color="red" width={20} height={20} />
                    ) : undefined}
                  </Group>
                </Card.Section>
                <Card.Section>
                  <Group justify="space-between">
                    <Center>
                      <Badge me="xs">{item.topic}</Badge>
                      <Badge color="grape">{item.level}</Badge>
                    </Center>
                    <Link
                      href={`/study/${id}?subjectDetailId=${subjectDetailId}&practiceId=${item.id}`}
                    >
                      <Button size="xs">Làm Bài</Button>
                    </Link>
                  </Group>
                </Card.Section>
              </Card>
            ))
          )
        ) : (
          <Alert>Không Có Bài Tập</Alert>
        )}
      </Paper>
    </StudyLayout>
  );
}
