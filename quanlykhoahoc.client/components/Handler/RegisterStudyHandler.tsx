"use client";

import { Button, Grid, TextInput, Title } from "@mantine/core";
import {
  IRegisterStudyMapping,
  RegisterStudyClient,
  RegisterStudyUpdate,
} from "../../app/web-api-client";
import useSWR from "swr";
import { SubjectSelect } from "../Helper/AppSelect";
import { useEffect, useState } from "react";
import Loading from "../Loading/Loading";
import { handleSubmit } from "../../lib/helper";

export default function RegisterStudyHandler({ userId }: { userId?: number }) {
  const RegisterStudyService = new RegisterStudyClient();

  const [registerStudies, setRegisterStudies] =
    useState<IRegisterStudyMapping[]>();

  const { data, isLoading } = useSWR(
    `/api/registerStudy/${userId}`,
    () =>
      RegisterStudyService.getEntities(null, userId, null, null, null, null),
    {
      revalidateIfStale: false,
      revalidateOnFocus: false,
      revalidateOnReconnect: false,
    }
  );

  useEffect(() => {
    if (data?.items) setRegisterStudies(data.items);
  }, [data]);

  return isLoading ? (
    <Loading />
  ) : (
    registerStudies?.map((item, index) => {
      return (
        <>
          <Grid.Col key={index}>
            <Title order={4}>Khóa Học</Title>
          </Grid.Col>
          <Grid.Col span={{ base: 6, lg: 3 }}>
            <TextInput
              value={item.course.name}
              label="Tên Khóa Học"
              labelProps={{ style: { marginBottom: 6 } }}
              readOnly
            />
          </Grid.Col>
          <Grid.Col span={{ base: 6, lg: 3 }}>
            <TextInput
              value={item.percentComplete}
              label="Phần Trăm Hoàn Thành"
              labelProps={{ style: { marginBottom: 6 } }}
              readOnly
            />
          </Grid.Col>
          <Grid.Col span={{ base: 6, lg: 3 }}>
            <SubjectSelect
              value={item.learningProgresses}
              onChange={(e) =>
                setRegisterStudies((prev) => {
                  const newStudies = [...prev];
                  newStudies[index] = {
                    ...newStudies[index],
                    learningProgresses: e,
                  };
                  return newStudies;
                })
              }
              courseId={item.courseId}
            />
          </Grid.Col>
          <Grid.Col span={{ base: 6, lg: 3 }}>
            <Button
              onClick={() =>
                handleSubmit(
                  () =>
                    new RegisterStudyClient().updateEntity(
                      item.id,
                      item as RegisterStudyUpdate
                    ),
                  "Sửa Thành Công"
                )
              }
            >
              Cập Nhật
            </Button>
          </Grid.Col>
        </>
      );
    })
  );
}
