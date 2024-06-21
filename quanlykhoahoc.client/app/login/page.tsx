"use client";

import { useToggle, upperFirst } from "@mantine/hooks";
import { useForm } from "@mantine/form";
import {
  TextInput,
  PasswordInput,
  Text,
  Paper,
  Group,
  Button,
  Divider,
  Anchor,
  Stack,
  Center,
  Container,
  Checkbox,
} from "@mantine/core";
import { IconBrandGoogle, IconBrandTwitter } from "@tabler/icons-react";
import { AuthClient, LoginRequest, RegisterRequest } from "../web-api-client";
import Cookies from "js-cookie";
import { useSelector } from "react-redux";
import { toast } from "react-toastify";
import { useRouter } from "next/navigation";
import { useEffect } from "react";
import ActionButton from "../../components/Helper/ActionButton";

export default function Login() {
  const AuthService = new AuthClient();

  const [type, toggle] = useToggle(["login", "register"]);

  const router = useRouter();

  const user = useSelector((state: any) => state.auth.user);

  const form = useForm({
    initialValues: {
      email: "",
      password: "",
      rePassword: "",
      terms: false,
    },
  });

  const handleSubmit = async () => {
    try {
      const { email, password, rePassword, terms } = form.values;

      if (type === "register") {
        if (!terms) {
          toast.error("Bạn Phải Đồng Ý Với Điều Khoản Và Dịch Vụ");
          return;
        }

        if (password !== rePassword) {
          toast.error("Mật Khẩu Phải Giống Nhau");
          return;
        }

        var r = await AuthService.register(
          new RegisterRequest({ email, password })
        );

        if (r.error) {
          toast.error(r.error);
          return;
        }
        
        toast("Vui Lòng Kiểm Tra Email Để Kích Hoạt Tài Khoản");
      }

      const response = await AuthService.login(
        new LoginRequest({ email, password })
      );

      if (!response) {
        toast.error("Vui Lòng Kiểm Tra Lại Thông Tin Đăng Nhập");
        return;
      }

      const { refreshToken } = response;

      if (refreshToken) {
        Cookies.set("refreshToken", refreshToken, { expires: 30 });
        setTimeout(() => {
          router.push("/");
        }, 1000);
      }
    } catch (error: any) {
      const errors = error.errors || {};
      for (const title in errors) {
        errors[title].forEach((errMsg: string) => {
          toast.warning(errMsg);
        });
      }
      throw error;
    }
  };

  useEffect(() => {
    if (user) router.push("/");
  }, [user, router]);

  return (
    !user && (
      <Container>
        <Center h={"100vh"}>
          <Paper radius="md" w={400} p="xl" withBorder>
            <Text size="lg" fw={500}>
              Chào Mừng Bạn Đến Với Siurum
            </Text>

            <Group grow mb="md" mt="md">
              <Button
                variant="default"
                leftSection={
                  <IconBrandGoogle radius="xl">Google</IconBrandGoogle>
                }
              ></Button>
              <Button
                variant="default"
                leftSection={
                  <IconBrandTwitter radius="xl">Twitter</IconBrandTwitter>
                }
              ></Button>
            </Group>

            <Divider
              label="Hoặc Đăng Nhập Với Tài Khoản"
              labelPosition="center"
              my="lg"
            />

            <form onSubmit={(e) => e.preventDefault()}>
              <Stack>
                <TextInput
                  label="Email"
                  placeholder="Nhập Email Của Bạn"
                  value={form.values.email}
                  onChange={(event) =>
                    form.setFieldValue("email", event.currentTarget.value)
                  }
                  radius="md"
                  labelProps={{ style: { marginBottom: "8px" } }}
                  withAsterisk
                />

                <PasswordInput
                  label="Mật Khẩu"
                  placeholder="Nhập Mật Khẩu Của Bạn"
                  value={form.values.password}
                  onChange={(event) =>
                    form.setFieldValue("password", event.currentTarget.value)
                  }
                  radius="md"
                  labelProps={{ style: { marginBottom: "8px" } }}
                  withAsterisk
                />
                {type === "register" && (
                  <PasswordInput
                    label="Nhập Lại Mật Khẩu"
                    placeholder="Nhập Mật Khẩu Của Bạn"
                    value={form.values.rePassword}
                    onChange={(event) =>
                      form.setFieldValue(
                        "rePassword",
                        event.currentTarget.value
                      )
                    }
                    radius="md"
                    labelProps={{ style: { marginBottom: "8px" } }}
                    withAsterisk
                  />
                )}
                {type === "register" && (
                  <Checkbox
                    label="Tôi Đồng Ý Với Điều Khoản Và Dịch Vụ"
                    checked={form.values.terms}
                    onChange={(event) =>
                      form.setFieldValue("terms", event.currentTarget.checked)
                    }
                  />
                )}
              </Stack>

              <Group justify="space-between" my="md">
                <Anchor
                  component="button"
                  type="button"
                  c="dimmed"
                  onClick={() => toggle()}
                  size="xs"
                >
                  {type === "register"
                    ? "Đã Có Tài Khoản? Đăng Nhập"
                    : "Chưa Có Tài Khoản? Đăng Ký"}
                </Anchor>
                <Anchor
                  component="button"
                  type="button"
                  c="dimmed"
                  onClick={() => toggle()}
                  size="xs"
                >
                  Quên Mật Khẩu
                </Anchor>
                <ActionButton
                  type="submit"
                  radius="md"
                  w="100%"
                  action={() =>
                    toast.promise(handleSubmit, {
                      pending: "Đợi Xíu",
                      error: "Vui Lòng Kiểm Tra Lại",
                    })
                  }
                >
                  {upperFirst(type === "login" ? "Đăng Nhập" : "Đăng Ký")}
                </ActionButton>
              </Group>
            </form>
          </Paper>
        </Center>
      </Container>
    )
  );
}
